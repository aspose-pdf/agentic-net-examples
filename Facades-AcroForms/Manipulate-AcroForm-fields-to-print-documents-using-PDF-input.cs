using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF with AcroForm
        const string filledPdf = "filled_output.pdf";  // PDF after fields are filled

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // ---------- Fill AcroForm fields ----------
            // Form implements IDisposable via SaveableFacade, so use a using block.
            using (Form form = new Form(inputPdf))
            {
                // Example: fill a text field, a checkbox and a list box.
                // Use the full field names as they appear in the PDF.
                form.FillField("CustomerName", "John Doe");
                form.FillField("AcceptTerms", true);               // check box
                form.FillField("CountryList", 2);                  // select third item (zero‑based index)

                // Save the modified document to a new file.
                form.Save(filledPdf);
            }

            // ---------- Print the filled PDF ----------
            // PdfViewer also implements IDisposable.
            using (PdfViewer viewer = new PdfViewer())
            {
                // Bind the filled PDF file.
                viewer.BindPdf(filledPdf);

                // Optional: configure printing options.
                viewer.AutoResize = true;      // fit to printable area
                viewer.AutoRotate = true;      // rotate pages if needed
                viewer.PrintPageDialog = false; // suppress page‑range dialog

                // Print using the default printer.
                viewer.PrintDocument();
            }

            Console.WriteLine($"Form fields filled and printed successfully. Output file: {filledPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}