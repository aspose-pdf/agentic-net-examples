using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List of PDF files to which the same XFDF comments will be applied
        string[] inputFiles = new string[] { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        string xfdfFile = "comments.xfdf";

        if (!File.Exists(xfdfFile))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfFile}");
            return;
        }

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPath}");
                continue;
            }

            using (Document pdfDoc = new Document(inputPath))
            {
                // Import all annotations (comments) from the XFDF file
                pdfDoc.ImportAnnotationsFromXfdf(xfdfFile);

                // Create a simple output filename – same name with suffix
                string outputFileName = Path.GetFileNameWithoutExtension(inputPath) + "_annotated.pdf";
                outputFileName = Path.GetFileName(outputFileName); // ensure no directory part

                pdfDoc.Save(outputFileName);
                Console.WriteLine($"Annotated PDF saved as '{outputFileName}'.");
            }
        }
    }
}