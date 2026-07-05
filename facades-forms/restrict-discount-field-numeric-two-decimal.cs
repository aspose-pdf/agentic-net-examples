using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "Discount";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF with FormEditor (Facades API)
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF document
            formEditor.BindPdf(inputPath);

            // Iterate through all pages and annotations to locate the target field
            foreach (Page page in formEditor.Document.Pages)
            {
                foreach (Annotation annotation in page.Annotations)
                {
                    // NumberField derives from TextBoxField and represents a numeric input field
                    if (annotation is NumberField numberField && numberField.Name == fieldName)
                    {
                        // Allow only digits and a single decimal point
                        numberField.AllowedChars = "0123456789.";

                        // Optional: limit the total number of characters (e.g., 10 characters)
                        numberField.MaxLen = 10;

                        // No further action needed; the field now restricts input to numeric values
                    }
                }
            }

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field \"{fieldName}\" configured and saved to '{outputPath}'.");
    }
}