using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string txtPath = "output.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Retrieve the TextBox field named "textbox1"
                TextBoxField textBox = doc.Form["textbox1"] as TextBoxField;
                if (textBox == null)
                {
                    Console.Error.WriteLine("TextBox field 'textbox1' not found.");
                    return;
                }

                // Get the field value (null-coalesced to empty string)
                string fieldValue = textBox.Value ?? string.Empty;

                // Save the value to a plain text file
                File.WriteAllText(txtPath, fieldValue);
                Console.WriteLine($"Field value saved to '{txtPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}