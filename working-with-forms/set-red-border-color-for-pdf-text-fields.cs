using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // needed for Border class

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields in the document
            foreach (Field field in doc.Form)
            {
                // Process only text box fields (including derived types)
                if (field is TextBoxField txtField)
                {
                    // Ensure a Border object exists (required constructor takes the parent annotation)
                    if (txtField.Border != null)
                    {
                        // Optionally set border width; color is NOT a Border property
                        txtField.Border.Width = 1;
                    }
                    else
                    {
                        txtField.Border = new Border(txtField) { Width = 1 };
                    }

                    // Set the border (and background) color via the field's own Color property
                    txtField.Color = Aspose.Pdf.Color.Red;
                }
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
