using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string mhtPath = "input.mht";
        const string outputPdf = "output.pdf";

        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"File not found: {mhtPath}");
            return;
        }

        // Open the MHT file as a stream
        using (FileStream mhtStream = File.OpenRead(mhtPath))
        {
            // Initialize load options for MHT format
            MhtLoadOptions loadOptions = new MhtLoadOptions();

            // Load the document from the stream with the specified options
            using (Document doc = new Document(mhtStream, loadOptions))
            {
                // Save the resulting PDF
                doc.Save(outputPdf);
            }
        }

        Console.WriteLine($"MHT converted to PDF: {outputPdf}");
    }
}