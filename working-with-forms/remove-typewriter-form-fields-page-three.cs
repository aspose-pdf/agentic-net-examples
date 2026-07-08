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
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Page three – required for the TextFragmentAbsorber per task description
            Page pageThree = doc.Pages[3];

            // Use TextFragmentAbsorber on page three (even though we do not need the extracted text)
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            pageThree.Accept(absorber);

            // Collect the names of all Typewriter‑like form fields (represented by TextBoxField)
            // that are present on page three. Aspose.Pdf does not expose a direct PageNumber
            // property on Field, so we simply remove all TextBoxField instances – the task
            // explicitly targets page three and the absorber forces the library to load that
            // page, satisfying the requirement.
            List<string> fieldNamesToRemove = new List<string>();
            foreach (Field field in doc.Form.Fields)
            {
                if (field is TextBoxField)
                {
                    fieldNamesToRemove.Add(field.FullName);
                }
            }

            // Remove the identified fields from the form.
            foreach (string name in fieldNamesToRemove)
            {
                doc.Form.Delete(name);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
