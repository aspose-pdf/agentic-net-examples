using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string stampImage = "stamp.png";
        const string outputPdf  = "stamped_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPdf))
            {
                // Optional: display existing AcroForm field values before stamping
                var form = pdfDocument.Form;
                if (form != null && form.Fields != null && form.Fields.Count() > 0)
                {
                    foreach (Field field in form.Fields)
                    {
                        string name  = field.FullName ?? field.Name ?? string.Empty;
                        string value = field.Value?.ToString() ?? string.Empty;
                        Console.WriteLine($"Field '{name}' value before stamp: {value}");
                    }
                }

                // Create an ImageStamp; default behavior does NOT flatten the page,
                // so AcroForm fields remain intact.
                ImageStamp imgStamp = new ImageStamp(stampImage)
                {
                    // Example positioning: bottom‑right corner of each page
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment   = VerticalAlignment.Bottom
                };

                // Apply the stamp to each page individually (per‑page AddStamp)
                foreach (Page page in pdfDocument.Pages)
                {
                    page.AddStamp(imgStamp);
                }

                // Save the modified PDF; AcroForm fields are preserved.
                pdfDocument.Save(outputPdf);
            }

            Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
