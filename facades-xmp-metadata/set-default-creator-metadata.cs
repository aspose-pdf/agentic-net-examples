using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // required for PdfFileInfo type

class ConfigurablePdfCreator
{
    /// <summary>
    /// Ensures that the PDF has a Creator metadata entry.
    /// If the source PDF does not contain a Creator value, the provided defaultCreator is applied.
    /// The method works for both existing PDFs and newly created empty PDFs.
    /// </summary>
    /// <param name="inputPath">Path to the source PDF. If null or empty, a new PDF is created.</param>
    /// <param name="outputPath">Path where the resulting PDF will be saved.</param>
    /// <param name="defaultCreator">Default Creator string to use when none is present.</param>
    public static void ApplyDefaultCreator(string inputPath, string outputPath, string defaultCreator)
    {
        // Validate parameters
        if (string.IsNullOrWhiteSpace(outputPath))
            throw new ArgumentException("Output path must be provided.", nameof(outputPath));

        if (string.IsNullOrWhiteSpace(defaultCreator))
            throw new ArgumentException("Default creator must be provided.", nameof(defaultCreator));

        // Use a using block for deterministic disposal (document-disposal-with-using rule)
        using (Document doc = string.IsNullOrWhiteSpace(inputPath) ? new Document() : new Document(inputPath))
        {
            // If the document is newly created, add a blank page so it is a valid PDF
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Retrieve current Creator metadata
            string currentCreator = doc.Info.Creator;

            // If no Creator is set, apply the default value
            if (string.IsNullOrWhiteSpace(currentCreator))
            {
                // PdfFileInfo is part of Aspose.Pdf.Facades; we can use it to set the Creator
                PdfFileInfo fileInfo = new PdfFileInfo();
                fileInfo.Creator = defaultCreator;

                // Transfer the value to the Document's Info object
                doc.Info.Creator = fileInfo.Creator;
            }

            // Save the document (save-to-non-pdf-always-use-save-options rule not needed for PDF)
            doc.Save(outputPath);
        }
    }

    // Example usage
    static void Main()
    {
        const string inputPdf = "source.pdf";      // Set to null or "" to create a new PDF
        const string outputPdf = "result.pdf";
        const string defaultCreator = "MyApp PDF Generator";

        try
        {
            ApplyDefaultCreator(inputPdf, outputPdf, defaultCreator);
            Console.WriteLine($"PDF saved to '{outputPdf}' with Creator set to '{defaultCreator}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}