using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize FormEditor facade and bind the PDF
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath); // load the document

            // Access the underlying Document object
            Document doc = formEditor.Document;

            // JavaScript that validates two‑decimal numeric input
            string js = @"if (!/^\d+(\.\d{1,2})?$/.test(event.value)) { app.alert('Please enter a numeric value with up to two decimal places.'); event.rc = false; }";

            // Locate the NumberField named "Discount" and configure it
            foreach (Page page in doc.Pages)
            {
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is NumberField numberField && numberField.Name == "Discount")
                    {
                        // Allow only digits and a single decimal point
                        numberField.AllowedChars = "0123456789.";
                        // Limit total characters (e.g., up to 7 characters for 9999.99)
                        numberField.MaxLen = 7;
                    }
                }
            }

            // Attach the validation script to the field using FormEditor
            formEditor.SetFieldScript("Discount", js);

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
