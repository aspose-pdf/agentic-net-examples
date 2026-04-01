using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Load existing XMP metadata from the PDF
        PdfXmpMetadata xmpMetadata = new PdfXmpMetadata();
        xmpMetadata.BindPdf(inputPath);

        // Define the XMP property to set and the new value
        DefaultMetadataProperties propertyKey = DefaultMetadataProperties.Nickname;
        string newValue = "NewNickname";

        // Retrieve the current value (if any)
        XmpValue existingValue = xmpMetadata[propertyKey];
        bool hasExisting = existingValue != null && !string.IsNullOrEmpty(existingValue.ToString());

        if (hasExisting)
        {
            Console.WriteLine("Warning: XMP property '" + propertyKey + "' already has a value: " + existingValue);
        }
        else
        {
            // Add the new value because the field is empty
            xmpMetadata.Add(propertyKey, newValue);
        }

        // Save the PDF with the (potentially) updated XMP metadata
        xmpMetadata.Save(outputPath);
        Console.WriteLine("PDF saved to '" + outputPath + "'.");
    }
}
