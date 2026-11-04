using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Create a simple PDF if the input file does not exist
        if (!File.Exists(inputPath))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // add a blank page
                doc.Save(inputPath);
            }
        }

        try
        {
            // Open the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Ensure the document has an AcroForm (created automatically if missing)
                Form form = doc.Form;

                // Define the rectangle for the text box (use fully qualified type to avoid ambiguity)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

                // Create a text box field on the first page
                TextBoxField txtField = new TextBoxField(doc.Pages[1], rect)
                {
                    PartialName = "MyTextBox",
                    Value = "Enter text here"
                };

                // Enable spell checking for the text box (supported by the library)
                txtField.SpellCheck = true;

                // Add the field to the form on page 1
                form.Add(txtField, 1);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with spell‑checking enabled: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}