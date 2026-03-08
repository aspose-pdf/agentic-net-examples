using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf  = "TemplateForm.pdf";
        const string outputPdf = "FilledForm.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use the Form facade to work with AcroForm fields.
        // The Form class implements IDisposable via SaveableFacade, so wrap it in a using block.
        using (Form form = new Form(inputPdf))
        {
            // Fill simple text fields
            form.FillField("FirstName", "John");
            form.FillField("LastName",  "Doe");
            form.FillField("Email",     "john.doe@example.com");

            // Fill a checkbox – true checks the box, false unchecks it
            form.FillField("AgreeTerms", true);

            // Fill a radio button group by index (0‑based). Example: 0 = Male, 1 = Female
            form.FillField("Gender", 0); // selects "Male"

            // Fill a list box by index (0‑based). Example: 2 = Canada
            form.FillField("Country", 2);

            // Fill a combo box by index (0‑based). Example: 1 = "Engineer"
            form.FillField("Occupation", 1);

            // Fill a multi‑selection list box with multiple values
            form.FillField("Interests", new string[] { "Reading", "Traveling", "Music" });

            // Fill a text field and force the font size to fit the box
            form.FillField("Comments", "This is a sample comment that will be resized to fit.", true);

            // Save the updated PDF to the output file.
            // Save(string) writes a PDF regardless of the file extension.
            form.Save(outputPdf);
        }

        Console.WriteLine($"Form filled and saved to '{outputPdf}'.");
    }
}