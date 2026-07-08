using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPdf = "output.pdf"; // PDF with confirmation script added

        // 1. Create a minimal source PDF in memory (so we never depend on an external file).
        using (var sourceStream = new MemoryStream())
        {
            // Create a blank PDF with one page.
            using (var doc = new Document())
            {
                doc.Pages.Add(); // add a default A4 page
                // Save the document to the memory stream.
                doc.Save(sourceStream);
            }

            // Reset the stream position before reading it again.
            sourceStream.Position = 0;

            // 2. JavaScript that shows a confirmation dialog before submitting the form.
            // app.alert returns 4 when the user clicks "Yes" in a 3‑button dialog.
            string confirmScript = "if(app.alert('Are you sure you want to submit?', 3) == 4) this.submitForm();";

            // 3. Use FormEditor with the stream overload to avoid file‑system dependencies.
            using (FormEditor editor = new FormEditor())
            {
                // Bind the PDF from the memory stream.
                editor.BindPdf(sourceStream);

                // Add a submit button on page 1 (coordinates are in points).
                // Signature of AddSubmitBtn: (string fieldName, int pageNumber, string fieldName, string buttonLabel,
                //                           float llx, float lly, float urx, float ury)
                editor.AddSubmitBtn(
                    "SubmitBtn",   // field/button name
                    1,              // page number (1‑based)
                    "SubmitBtn",   // field name (same as above)
                    "Submit",      // button label displayed on the PDF
                    100f,
                    100f,
                    200f,
                    150f);

                // Attach the JavaScript to the button.
                editor.AddFieldScript("SubmitBtn", confirmScript);

                // Save the modified PDF directly to disk.
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF with confirmation dialog saved to '{outputPdf}'.");
    }
}
