using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string nickname = "CustomIdentifier123";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure any previous instance of the output file is removed so the build process
        // does not try to overwrite a file that is still locked by a running process.
        try
        {
            if (File.Exists(outputPdf))
                File.Delete(outputPdf);
        }
        catch (IOException ex)
        {
            Console.Error.WriteLine($"Unable to delete existing output file: {ex.Message}");
            // Continue – the Save method will overwrite if possible.
        }

        // Bind the PDF, add the Nickname XMP property, and save the result.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPdf);                 // Load the PDF into the facade.
            xmp.Add("xmp:Nickname", nickname);    // Set the Nickname property.
            xmp.Save(outputPdf);                   // Save the PDF with updated XMP metadata.
        }

        Console.WriteLine($"Nickname '{nickname}' added to XMP metadata and saved as '{outputPdf}'.");
    }
}
