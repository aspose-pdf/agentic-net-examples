using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // <-- required for form field types

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_qr.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Build a string that contains the PDF metadata we want to encode
            // (Title, Author, Subject, Keywords). Any missing value is ignored.
            string metadata = "";
            if (!string.IsNullOrEmpty(doc.Info.Title))   metadata += $"Title:{doc.Info.Title};";
            if (!string.IsNullOrEmpty(doc.Info.Author))  metadata += $"Author:{doc.Info.Author};";
            if (!string.IsNullOrEmpty(doc.Info.Subject)) metadata += $"Subject:{doc.Info.Subject};";
            if (!string.IsNullOrEmpty(doc.Info.Keywords))metadata += $"Keywords:{doc.Info.Keywords};";

            // If there is no metadata, use a fallback value
            if (string.IsNullOrEmpty(metadata))
                metadata = "NoMetadata";

            // Iterate over all pages and add a QR‑code barcode field to each page.
            // The barcode is drawn into a TextBoxField using AddBarcode().
            // The field is placed at a fixed position (50,50) with a size of 100x100 points.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Get the current page reference
                Page page = doc.Pages[i];

                // Create a new TextBoxField that will hold the QR code.
                // Correct ctor: TextBoxField(Page, Rectangle)
                TextBoxField qrField = new TextBoxField(page, new Aspose.Pdf.Rectangle(50, 50, 150, 150));
                qrField.PartialName = $"qrField_page_{i}";

                // Generate the QR code from the metadata string
                qrField.AddBarcode(metadata);

                // Add the field to the document's form (specify the page index for safety)
                doc.Form.Add(qrField, i);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with QR‑code stamp saved to '{outputPath}'.");
    }
}
