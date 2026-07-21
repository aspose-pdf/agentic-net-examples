using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Access the AcroForm object
                Form form = doc.Form;
                int fieldLimit = 0;

                // Iterate through all form fields and find the one named "Address"
                foreach (Field f in form.Fields)
                {
                    if (f.Name == "Address" && f is TextBoxField txtField)
                    {
                        // MaxLen holds the character limit for a text box field
                        fieldLimit = txtField.MaxLen;
                        break;
                    }
                }

                Console.WriteLine($"Current character limit of the 'Address' field: {fieldLimit}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
