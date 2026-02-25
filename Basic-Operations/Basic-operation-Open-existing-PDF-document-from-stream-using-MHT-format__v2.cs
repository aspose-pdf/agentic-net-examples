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
            // Options for loading an MHT file
            MhtLoadOptions loadOptions = new MhtLoadOptions();

            // Load the document from the stream using the MHT options
            using (Document doc = new Document(mhtStream, loadOptions))
            {
                // Save the loaded document as PDF (Document.Save without options writes PDF)
                doc.Save(outputPdf);
            }
        }

        Console.WriteLine($"MHT file converted to PDF: {outputPdf}");
    }
}