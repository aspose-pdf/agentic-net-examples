using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "myTextField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Locate the field in the form's Fields collection and cast to TextBoxField
                var textField = doc.Form?.Fields?.FirstOrDefault(f => f.Name == fieldName) as TextBoxField;
                if (textField != null)
                {
                    // Set font size
                    textField.DefaultAppearance.FontSize = 14;

                    // Set font (Bold) – retrieve the font and assign its name
                    var boldFont = FontRepository.FindFont("Arial", FontStyles.Bold);
                    if (boldFont != null)
                    {
                        textField.DefaultAppearance.FontName = boldFont.FontName;
                    }
                    else
                    {
                        // Fallback to regular Arial if bold variant is not found
                        textField.DefaultAppearance.FontName = "Arial";
                    }

                    // Set font color for better readability (System.Drawing.Color is required)
                    textField.DefaultAppearance.TextColor = System.Drawing.Color.Black;
                }
                else
                {
                    Console.Error.WriteLine($"Form field '{fieldName}' not found or is not a text box.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with updated font settings to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
