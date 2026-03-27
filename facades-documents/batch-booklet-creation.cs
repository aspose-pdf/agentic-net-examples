using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string[] inputFiles = { "input1.pdf", "input2.pdf", "input3.pdf" };
        string logFilePath = "booklet_creation_log.txt";

        using (StreamWriter logWriter = new StreamWriter(logFilePath, false))
        {
            foreach (string inputFile in inputFiles)
            {
                if (!File.Exists(inputFile))
                {
                    logWriter.WriteLine(string.Format("{0}: Input file not found: {1}", DateTime.Now, inputFile));
                    continue;
                }

                string outputFile = Path.GetFileNameWithoutExtension(inputFile) + "_booklet.pdf";

                logWriter.WriteLine(string.Format("{0}: Starting booklet creation for {1} -> {2}", DateTime.Now, inputFile, outputFile));

                PdfFileEditor pdfEditor = new PdfFileEditor();
                bool result = pdfEditor.TryMakeBooklet(inputFile, outputFile);

                if (result)
                {
                    logWriter.WriteLine(string.Format("{0}: Successfully created booklet: {1}", DateTime.Now, outputFile));
                }
                else
                {
                    logWriter.WriteLine(string.Format("{0}: Failed to create booklet for {1}", DateTime.Now, inputFile));
                }
            }

            logWriter.WriteLine(string.Format("{0}: Batch processing completed.", DateTime.Now));
        }
    }
}