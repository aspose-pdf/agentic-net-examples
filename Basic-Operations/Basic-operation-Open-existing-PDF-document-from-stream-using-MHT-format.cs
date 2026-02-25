using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string mhtPath = "input.mht";
        const string pdfPath = "output.pdf";

        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"File not found: {mhtPath}");
            return;
        }

        // Open the MHT file as a stream and load it with MhtLoadOptions
        using (FileStream mhtStream = File.OpenRead(mhtPath))
        {
            MhtLoadOptions loadOptions = new MhtLoadOptions();

            // Load the MHT content into a PDF Document
            using (Document pdfDoc = new Document(mhtStream, loadOptions))
            {
                // Save the resulting PDF document
                pdfDoc.Save(pdfPath);
            }
        }

        Console.WriteLine($"MHT converted to PDF: {pdfPath}");
    }
}