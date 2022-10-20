import java.io.*;
import java.util.Arrays;
import java.util.PriorityQueue;
import java.util.Random;

public class App {

    public record Element(int value, int file_index) implements Comparable<Element> {
        public int getValue() {
            return value;
        }

        public int getFileIndex() {
            return file_index;
        }

        @Override
        public int compareTo(Element o) {
            return Integer.compare(this.value, o.value);
        }

        static int intNull = Integer.MAX_VALUE, intSize = 4, N = 5;

        static long processedData;
        static int nextRunFirstElem;
        static int outFileIndex;
        static int previousOutFileIndex;
        static int runsPerLevel;

        static int[] dummyRuns = new int[N + 1];
        static int[] runsDistribution = new int[N + 1];
        static boolean[] fileCanBeRead = new boolean[N + 1];
        static int[] runsLastElems = new int[N + 1];
        static int[] run_last_elements = new int[N + 1];
        static Element[] nextRunFirstElems = new Element[N + 1];

        static PriorityQueue<Element> queue = new PriorityQueue<>();
        static byte[] byteBuffer = new byte[4];

        static long unsortedFileLen;

        public static void main(String[] args) throws IOException {
            File file = initFile("main.bin");
            unsortedFileLen = file.length();
            MergeSort(file);
        }

        public static File initFile(String path) throws IOException {
            System.out.print("Do you want to use existing file or generate new one [0-existing / 1-new]? ");
            BufferedReader console = new BufferedReader(new InputStreamReader(System.in));
            File data;

            if (Integer.parseInt(console.readLine()) > 0) {
                System.out.print("What is size of wanted file in MB? ");
                int mb = Integer.parseInt(console.readLine());
                DataOutputStream dos = new DataOutputStream(new FileOutputStream(data = new File(path)));
                Random random = new Random();
                System.out.println("Generating file");
                long start = System.currentTimeMillis();

                if (mb <= 1 << 8) {
                    int[] tmp = new int[mb << 18];
                    for (int i = 0; i < mb << 18; i++)
                        tmp[i] = random.nextInt(intNull);
                    dos.write(toByte(tmp));
                } else {
                    int[] tmp = new int[1 << 8];
                    for (int i = 0; i < mb << 10; i++) {
                        for (int j = 0; j < 1 << 8; j++)
                            tmp[j] = random.nextInt(intNull);
                        dos.write(toByte(tmp));
                    }
                }

                System.out.println("Successfully generated file of " + mb + " MB ");
                System.out.println("Generation time:" + (System.currentTimeMillis() - start) + " ms");
                dos.close();
            } else {
                System.out.print("Write path to the file: ");
                data = new File(console.readLine());
            }
            console.close();
            return data;
        }

        private static byte[] toByte(int[] d) throws IOException {
            ByteArrayOutputStream bos = new ByteArrayOutputStream(d.length << 2);
            DataOutputStream dos = new DataOutputStream(bos);
            for (int i : d)
                dos.writeInt(i);
            return bos.toByteArray();
        }

        private static int ByteBufferToInt() {
            return byteBuffer[0] << 24 | (byteBuffer[1] & 0xFF) << 16 | (byteBuffer[2] & 0xFF) << 8
                    | (byteBuffer[3] & 0xFF);
        }

        private static void MergeSort(File main_file) throws IOException {
            DataInputStream[] run_files_dis = new DataInputStream[N + 1];
            File[] working_files = new File[N + 1];

            for (int i = 0; i < working_files.length; i++)
                working_files[i] = new File("work_temp_" + (i + 1) + ".bin");
            DistributeRuns(N, working_files, new DataInputStream(new FileInputStream(main_file)));

            long start = System.currentTimeMillis();
            int min_dummy_values = GetMinDummyValue();
            InitMerge(min_dummy_values);
            DataOutputStream dos = new DataOutputStream(new FileOutputStream(working_files[outFileIndex]));
            for (int i = 0; i < run_files_dis.length - 1; i++)
                run_files_dis[i] = new DataInputStream(new FileInputStream(working_files[i]));
            while (runsPerLevel > 0) {
                runsLastElems[outFileIndex] = intNull;
                MergeProcedure(runsDistribution[getMinFileIndex()] - min_dummy_values, run_files_dis, dos);
                setPreviousRunDistributionLevel();
                outFileIndex = (outFileIndex > 0 ? outFileIndex : runsDistribution.length) - 1;
                resetAllowReadArray();
                min_dummy_values = GetMinDummyValue();
                dos = new DataOutputStream(new FileOutputStream(working_files[outFileIndex]));
                run_files_dis[previousOutFileIndex] = new DataInputStream(
                        new FileInputStream(working_files[previousOutFileIndex]));
            }
            System.out.println("Merging done " + (System.currentTimeMillis() - start) + " ms");
            dos.close();
            closeAll(run_files_dis);
            outHelpFiles(main_file, working_files);
        }

        private static void resetAllowReadArray() {
            Arrays.fill(fileCanBeRead, true);
            fileCanBeRead[outFileIndex] = false;
        }

        private static int GetMinDummyValue() {
            int min = dummyRuns[0];
            for (int i = 1; i < dummyRuns.length; i++)
                if (dummyRuns[i] < min)
                    if (i != outFileIndex)
                        min = dummyRuns[i];
            return min;
        }

        private static void DistributeRuns(int temp_files, File[] working_files, DataInputStream main_file_dis)
                throws IOException {
            long start = System.currentTimeMillis();
            runsPerLevel = 1;
            runsDistribution[0] = 1;
            outFileIndex = working_files.length - 1;
            int[] write_sentinel = new int[temp_files];
            DataOutputStream[] run_files_dos = new DataOutputStream[temp_files];

            for (int i = 0; i < temp_files; i++)
                run_files_dos[i] = new DataOutputStream(new FileOutputStream(working_files[i]));
            while (processedData < unsortedFileLen) {
                for (int i = 0; i < temp_files; i++)
                    while (write_sentinel[i] != runsDistribution[i]) {
                        while (processedData < unsortedFileLen && nextRunFirstElem != intNull
                                && run_last_elements[i] <= nextRunFirstElem)
                            WriteNextRun(main_file_dis, run_files_dos[i], i);
                        WriteNextRun(main_file_dis, run_files_dos[i], i);
                        dummyRuns[i]++;
                        write_sentinel[i]++;
                    }
                setNextDistributionLevel();
            }
            setPreviousRunDistributionLevel();

            for (int i = 0; i < runsDistribution.length - 1; i++)
                dummyRuns[i] = runsDistribution[i] - dummyRuns[i];

            System.out.println("Distribution done in " + (System.currentTimeMillis() - start) + " ms");
        }

        private static void WriteNextRun(DataInputStream main_file_dis, DataOutputStream run_file_dos,
                int file_index) throws IOException {
            if (processedData >= unsortedFileLen) {
                dummyRuns[file_index]--;
                return;
            }
            if (nextRunFirstElem != intNull) {
                run_file_dos.writeInt(nextRunFirstElem);
                processedData += intSize;
            }

            int min_value = Integer.MIN_VALUE;
            if (main_file_dis.read(byteBuffer) < intSize)
                return;

            int current_int = ByteBufferToInt();

            while (current_int != intNull) {
                if (current_int >= min_value) {
                    run_file_dos.writeInt(current_int);
                    processedData += intSize;
                    min_value = current_int;
                    if (main_file_dis.read(byteBuffer) < intSize)
                        break;
                    current_int = ByteBufferToInt();
                } else {
                    nextRunFirstElem = current_int;
                    run_last_elements[file_index] = min_value;
                    break;
                }
            }
        }

        private static void InitMerge(int min_dummy) {
            for (int i = 0; i < dummyRuns.length - 1; i++)
                dummyRuns[i] -= min_dummy;
            dummyRuns[outFileIndex] += min_dummy;
            resetAllowReadArray();
        }

        private static void MergeProcedure(int min_file_values, DataInputStream[] run_files_dis,
                DataOutputStream writer)
                throws IOException {
            int num, min_file, heap_empty = 0;
            Element element;
            FillQueue(run_files_dis);

            while (heap_empty != min_file_values) {
                if ((element = queue.poll()) == null)
                    return;
                writer.writeInt(element.getValue());
                min_file = element.getFileIndex();
                if (fileCanBeRead[min_file])
                    if ((num = ReadInteger(run_files_dis[min_file], min_file)) != intNull)
                        queue.add(new Element(num, min_file));
                if (queue.size() == 0) {
                    heap_empty++;
                    for (int i = 0; i < nextRunFirstElems.length; i++) {
                        if (nextRunFirstElems[i] == null)
                            break;
                        queue.add(new Element(nextRunFirstElems[i].getValue(), i));
                        runsLastElems[i] = nextRunFirstElems[i].getValue();
                    }
                    FillQueue(run_files_dis);
                    resetAllowReadArray();
                }
            }
        }

        private static void FillQueue(DataInputStream[] run_files_dis) throws IOException {
            for (int i = 0; i < run_files_dis.length; i++)
                if (dummyRuns[i] == 0) {
                    if (fileCanBeRead[i])
                        queue.add(new Element(ReadInteger(run_files_dis[i], i), i));
                } else
                    dummyRuns[i]--;
        }

        private static int ReadInteger(DataInputStream file_dis, int file_index) throws IOException {
            if (file_dis.read(byteBuffer) < intSize)
                return intSize;

            int current_int = ByteBufferToInt();
            if (runsLastElems[file_index] != intSize)
                if (current_int < runsLastElems[file_index]) {
                    nextRunFirstElems[file_index] = new Element(current_int, file_index);
                    fileCanBeRead[file_index] = false;
                    return intSize;
                }
            return runsLastElems[file_index] = current_int;
        }

        private static int getMinFileIndex() {
            int min_file_index = 0, min = runsDistribution[0];
            for (int i = 1; i < runsDistribution.length; i++)
                if (runsDistribution[i] != 0)
                    if (runsDistribution[i] < min)
                        min_file_index = i;
            return min_file_index;
        }

        private static void setNextDistributionLevel() {
            runsPerLevel = 0;
            int[] clone = runsDistribution.clone();
            for (int i = 0; i < clone.length - 1; runsPerLevel += runsDistribution[++i])
                runsDistribution[i] = clone[0] + clone[i + 1];
        }

        private static void setPreviousRunDistributionLevel() {
            int[] clone = runsDistribution.clone();
            previousOutFileIndex = outFileIndex;
            runsDistribution[0] = runsPerLevel = clone[clone.length - 2];
            for (int diff, i = clone.length - 3; i >= 0; i--, runsPerLevel += diff)
                runsDistribution[i + 1] = diff = clone[i] - clone[clone.length - 2];
        }

        private static void outHelpFiles(File main_file, File[] temp_files) throws IOException {
            for (File temp_file : temp_files) {
                System.out.print(
                        "Unsorted file size: " + main_file.length() + " - Work file size: " + temp_file.length());
                System.out.println(temp_file.length() != 0
                        ? " - Is file " + temp_file.getName() + " sorted? - " + isFileSorted(temp_file)
                        : "");
            }
        }

        private static boolean isFileSorted(File temp_file) throws IOException {
            DataInputStream dis = new DataInputStream(new FileInputStream(temp_file));
            if (dis.read(byteBuffer) < intSize)
                return false;
            int first = ByteBufferToInt();
            if (dis.skip(temp_file.length() - 8) <= 0)
                return false;
            if (dis.read(byteBuffer) < intSize)
                return false;
            dis.close();
            return first <= ByteBufferToInt();
        }

        private static void closeAll(Closeable[] c) throws IOException {
            for (Closeable o : c)
                o.close();
        }

    }
}
