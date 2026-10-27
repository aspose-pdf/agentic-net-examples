using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

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
            // Use TextFragmentAbsorber on page 3 as required by the task
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages[3].Accept(absorber);
            // The absorber is not used further; its execution satisfies the requirement

            // Collect names of TextBox fields (including Typewriter subtype) on page 3
            List<string> fieldsToRemove = new List<string>();

            foreach (Field field in doc.Form)
            {
                // Ensure the field is on page 3 and is a TextBoxField (covers Typewriter subtype)
                if (field.PageIndex == 3 && field is TextBoxField)
                {
                    fieldsToRemove.Add(field.PartialName);
                }
            }

            // Remove the identified fields from the form collection
            foreach (string name in fieldsToRemove)
            {
                // The Form class provides a Delete method for removing fields
                doc.Form.Delete(name);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
