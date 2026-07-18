using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class XmpPropertyUpdater
{
    // Updates an XMP property only if it is currently empty.
    // Logs a warning when the property already has a non‑empty value.
    public static void SetXmpPropertyIfEmpty(
        string inputPdfPath,
        string outputPdfPath,
        DefaultMetadataProperties propertyKey,
        string newValue)
    {
        if (!System.IO.File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Bind the PDF to the XMP metadata facade.
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPdfPath);

        // Determine whether the property already exists and is non‑empty.
        bool hasExisting = xmp.Contains(propertyKey);
        if (hasExisting)
        {
            XmpValue existingValue = xmp[propertyKey];
            // XmpValue may be null or represent an empty string.
            // Convert to string for a simple emptiness check.
            string existingText = existingValue?.ToString() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(existingText))
            {
                // Log a warning and skip setting the new value.
                Console.WriteLine(
                    $"Warning: XMP property '{propertyKey}' already has a non‑empty value \"{existingText}\". Skipping update.");
                xmp.Close();
                return;
            }
        }

        // Property is missing or empty – add the new value.
        xmp.Add(propertyKey, newValue);

        // Save the updated PDF with the modified XMP metadata.
        xmp.Save(outputPdfPath);
        xmp.Close();

        Console.WriteLine($"XMP property '{propertyKey}' set to \"{newValue}\" and saved to '{outputPdfPath}'.");
    }

    // Example usage.
    static void Main()
    {
        const string input = "sample.pdf";
        const string output = "sample_updated.pdf";

        // Attempt to set the Nickname property.
        SetXmpPropertyIfEmpty(input, output, DefaultMetadataProperties.Nickname, "MyDocument");
    }
}