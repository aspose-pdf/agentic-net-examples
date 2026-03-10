using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputMht = "input.mht";
        const string outputMht = "output.mht";

        if (!File.Exists(inputMht))
        {
            Console.Error.WriteLine($"Input file not found: {inputMht}");
            return;
        }

        // Load the MHT file into a PDF Document using MhtLoadOptions
        MhtLoadOptions loadOptions = new MhtLoadOptions();
        using (Document doc = new Document(inputMht, loadOptions))
        {
            // Bind a PdfFileInfo facade to the document to manipulate metadata
            PdfFileInfo info = new PdfFileInfo(doc);
            info.Title   = "Sample MHT Document";
            info.Author  = "John Doe";
            info.Subject = "Demonstration of metadata assignment";
            info.Keywords = "Aspose.Pdf, MHT, metadata";

            // Save the document back to MHT format.
            // The overload without a SaveFormat enum lets Aspose infer the format from the file extension.
            doc.Save(outputMht);
        }

        Console.WriteLine($"Metadata assigned and saved to '{outputMht}'.");
    }
}
