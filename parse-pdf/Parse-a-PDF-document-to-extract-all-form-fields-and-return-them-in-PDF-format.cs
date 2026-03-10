using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;          // Form and Field classes
using Aspose.Pdf.Text;           // TextFragment and Position

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "extracted_fields.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF containing form fields
        using (Document srcDoc = new Document(inputPath))
        {
            // Create a new PDF document that will hold the extracted field information
            using (Document outDoc = new Document())
            {
                // Add the first page where the field list will be written
                Page page = outDoc.Pages.Add();

                // Starting vertical position (top margin)
                double yPos = page.PageInfo.Height - 50;
                const double lineHeight = 15; // space between lines

                // Iterate over all form fields in the source document
                foreach (Field field in srcDoc.Form.Fields)
                {
                    // Build a line with field name and its current value
                    string line = $"Name: {field.FullName}, Value: {field.Value}";

                    // Create a text fragment for the line
                    TextFragment tf = new TextFragment(line);
                    tf.Position = new Position(50, yPos); // left margin = 50

                    // Add the fragment to the page
                    page.Paragraphs.Add(tf);

                    // Move down for the next line
                    yPos -= lineHeight;

                    // If we run out of space, add a new page and reset the Y position
                    if (yPos < 50)
                    {
                        page = outDoc.Pages.Add();
                        yPos = page.PageInfo.Height - 50;
                    }
                }

                // Save the resulting PDF containing the extracted fields
                outDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Form fields have been extracted to '{outputPath}'.");
    }
}