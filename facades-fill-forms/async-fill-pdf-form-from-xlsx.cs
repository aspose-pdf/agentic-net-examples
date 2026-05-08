using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Async entry point (C# 7.1+)
    static async Task Main(string[] args)
    {
        // Paths to the source XLSX file, PDF template, and the output PDF.
        const string xlsxPath      = "data.xlsx";
        const string pdfTemplatePath = "template.pdf";
        const string outputPdfPath = "filled.pdf";

        // -----------------------------------------------------------------
        // 1. Read the XLSX source asynchronously.
        //    This uses the built‑in .NET async file I/O API.
        // -----------------------------------------------------------------
        byte[] xlsxBytes;
        try
        {
            xlsxBytes = await File.ReadAllBytesAsync(xlsxPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read XLSX file: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // 2. Extract data from the XLSX byte array.
        //    For brevity, we mock the extraction with a simple dictionary.
        //    In a real scenario you would parse the XLSX (e.g., using
        //    a library such as ClosedXML or EPPlus) and populate the
        //    dictionary with field names and values.
        // -----------------------------------------------------------------
        var fieldValues = new System.Collections.Generic.Dictionary<string, string>
        {
            { "CustomerName", "John Doe" },
            { "InvoiceNumber", "INV-1001" },
            { "TotalAmount", "1234.56" }
            // Add more field/value pairs as needed.
        };

        // -----------------------------------------------------------------
        // 3. Load the PDF template using Aspose.Pdf.Document.
        //    The Document is wrapped in a using block for deterministic disposal.
        // -----------------------------------------------------------------
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplatePath}");
            return;
        }

        using (Document pdfDoc = new Document(pdfTemplatePath))
        {
            // -----------------------------------------------------------------
            // 4. Fill the PDF form fields using the Aspose.Pdf.Facades.Form class.
            //    The Form facade is also disposed via a using block.
            // -----------------------------------------------------------------
            using (Form form = new Form(pdfDoc))
            {
                foreach (var kvp in fieldValues)
                {
                    // Fill each field with the corresponding value.
                    // The FillField method accepts the field name and the value as a string.
                    form.FillField(kvp.Key, kvp.Value);
                }
            }

            // -----------------------------------------------------------------
            // 5. Save the filled PDF asynchronously.
            //    SaveAsync(string, CancellationToken) writes the PDF to disk
            //    without blocking the calling thread.
            // -----------------------------------------------------------------
            try
            {
                await pdfDoc.SaveAsync(outputPdfPath, CancellationToken.None);
                Console.WriteLine($"PDF successfully saved to '{outputPdfPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to save PDF: {ex.Message}");
            }
        }
    }
}