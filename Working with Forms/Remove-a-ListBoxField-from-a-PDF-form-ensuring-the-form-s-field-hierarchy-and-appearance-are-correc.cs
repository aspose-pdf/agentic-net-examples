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
        const string listBoxName = "myListBox";

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
                // Access the form object
                Form form = doc.Form;

                // Verify that the ListBox field exists (searches hierarchy as well)
                if (form.HasField(listBoxName, true))
                {
                    // Retrieve the field instance – the Form indexer returns a WidgetAnnotation,
                    // so we need to cast it to a Field before using Field‑specific members.
                    Field? field = form[listBoxName] as Field;
                    if (field != null)
                    {
                        // Remove all additional appearances of the field, if any
                        int appearanceCount = field.Appearance.Count;
                        for (int i = appearanceCount - 1; i >= 0; i--)
                        {
                            form.RemoveFieldAppearance(field, i);
                        }

                        // Delete the field from the form hierarchy
                        form.Delete(field);
                    }
                    else
                    {
                        // This should not happen because HasField returned true, but we guard against it.
                        Console.WriteLine($"The annotation named '{listBoxName}' is not a form field.");
                    }
                }
                else
                {
                    Console.WriteLine($"ListBox field '{listBoxName}' not found.");
                }

                // Save the updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"ListBox field removed and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
