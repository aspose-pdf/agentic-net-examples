using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF with AcroForm fields
        const string outputPdf = "output.pdf";  // destination PDF after manipulation

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Form facade handles AcroForm operations and also supports saving the document.
        // It implements IDisposable, so wrap it in a using block for deterministic disposal.
        using (Form form = new Form(inputPdf))
        {
            // Example: fill a text field
            form.FillField("FirstName", "John");

            // Example: fill a checkbox (true = checked, false = unchecked)
            form.FillField("SubscribeNewsletter", true);

            // Example: select an item in a list box or combo box by index (zero‑based)
            // Assuming the field "CountryList" exists and has at least three items.
            form.FillField("CountryList", 2);

            // Convert the document to PDF/A‑1B format (PDF/A is a PDF sub‑format).
            // The ConvertTo property expects a PdfFormat enum value.
            form.ConvertTo = PdfFormat.PDF_A_1B;

            // Save the modified (and converted) PDF.
            form.Save(outputPdf);
        }

        Console.WriteLine($"AcroForm fields processed and PDF saved to '{outputPdf}'.");
    }
}