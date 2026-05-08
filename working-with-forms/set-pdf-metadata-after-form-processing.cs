using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "filled_form.pdf";
        const string outputPdf = "filled_form_with_metadata.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF that contains the processed form fields
        using (Document doc = new Document(inputPdf))
        {
            // -------------------------------------------------
            // Form processing placeholder (e.g., fill fields)
            // -------------------------------------------------
            // Example: set a text field value
            // Form form = doc.Form;
            // if (form != null && form["Name"] != null)
            // {
            //     form["Name"].Value = "John Doe";
            // }

            // Set standard document metadata (author and title)
            doc.Info.Author = "Acme Corp.";
            doc.Info.Title  = "Customer Information Form";

            // Optionally, set tagged‑content metadata as well
            // The TaggedContent property returns a TaggedContent instance; no interface is required.
            var tagged = doc.TaggedContent;
            tagged.SetTitle("Customer Information Form");
            tagged.SetLanguage("en-US");

            // Save the updated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with metadata to '{outputPdf}'.");
    }
}
