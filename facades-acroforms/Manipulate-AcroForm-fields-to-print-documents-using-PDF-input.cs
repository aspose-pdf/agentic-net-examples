using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "template_form.pdf";   // PDF with AcroForm fields
        const string filledPdf = "filled_form.pdf";     // Output after filling fields

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ---------- Fill AcroForm fields ----------
        // Form implements IDisposable via SaveableFacade, so wrap in using.
        using (Form form = new Form(inputPdf))
        {
            // Fill a text field (full field name required)
            form.FillField("FirstName", "John");
            form.FillField("LastName",  "Doe");

            // Fill a check box field (true = checked)
            form.FillField("AgreeTerms", true);

            // Fill a radio button field by index (e.g., 0 = first option)
            form.FillField("Gender", 0); // selects first radio option

            // Save the modified PDF to a new file
            form.Save(filledPdf);
        }

        // ---------- Print the filled PDF ----------
        // PdfViewer also implements IDisposable, wrap in using.
        using (PdfViewer viewer = new PdfViewer())
        {
            // Bind the filled PDF file
            viewer.BindPdf(filledPdf);

            // Optional: set printing preferences
            viewer.AutoResize = true;      // fit to printable area
            viewer.AutoRotate = true;      // rotate if needed
            viewer.PrintPageDialog = false; // suppress page range dialog

            // Print using the default printer
            viewer.PrintDocument();

            // No explicit close needed; using will dispose.
        }

        Console.WriteLine($"Form fields filled and printed from '{filledPdf}'.");
    }
}