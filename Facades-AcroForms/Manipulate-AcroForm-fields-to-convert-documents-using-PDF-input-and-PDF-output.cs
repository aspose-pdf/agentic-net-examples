using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input_form.pdf";   // source PDF with AcroForm fields
        const string outputPdf = "filled_output.pdf"; // destination PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Wrap the Form facade in a using block for deterministic disposal
        using (Form form = new Form(inputPdf))
        {
            // Example: fill some known fields (use full field names as required)
            // Adjust field names and values to match your PDF template
            form.FillField("FirstName", "John");
            form.FillField("LastName",  "Doe");
            form.FillField("AgreeTerms", true);          // check box
            form.FillField("Country",   "USA");          // combo box
            form.FillField("Options",   new string[] { "Option1", "Option3" }); // list box multi‑select

            // Optionally flatten all fields so they become part of the page content
            form.FlattenAllFields();

            // Optionally set a PDF/A conversion format (e.g., PDF/A‑1B)
            // The ConvertTo property expects a PdfFormat enum value
            form.ConvertTo = PdfFormat.PDF_A_1B;

            // Save the modified PDF to the output path
            form.Save(outputPdf);
        }

        Console.WriteLine($"AcroForm processing completed. Output saved to '{outputPdf}'.");
    }
}