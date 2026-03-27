using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            PdfFileEditor editor = new PdfFileEditor();
            MemoryStream[] pageStreams = editor.SplitToPages(inputStream);

            for (int i = 0; i < pageStreams.Length; i++)
            {
                MemoryStream pageStream = pageStreams[i];
                string outputFileName = "page" + (i + 1).ToString() + ".pdf";

                using (FileStream outputFile = new FileStream(outputFileName, FileMode.Create, FileAccess.Write))
                {
                    pageStream.WriteTo(outputFile);
                }

                Console.WriteLine("Saved page " + (i + 1).ToString() + " to " + outputFileName);
                pageStream.Dispose();
            }
        }
    }
}
