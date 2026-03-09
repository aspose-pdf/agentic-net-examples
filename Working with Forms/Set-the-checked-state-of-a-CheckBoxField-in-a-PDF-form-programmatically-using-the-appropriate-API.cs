using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document doc = new Document(inputPath))
            {
                // Locate the first checkbox field in the form
                CheckboxField checkbox = null;
                foreach (Field field in doc.Form.Fields)
                {
                    if (field is CheckboxField cb)
                    {
                        checkbox = cb;
                        break;
                    }
                }

                if (checkbox == null)
                {
                    Console.Error.WriteLine("No checkbox field found in the document.");
                }
                else
                {
                    // Set the checked state (true = checked, false = unchecked)
                    checkbox.Checked = true;
                    // Alternatively: checkbox.Value = "On";
                }

                // Save the modified PDF (PDF format, no SaveOptions needed)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}