using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // Annotation base class
using Aspose.Pdf.Forms;        // Field and JavascriptAction classes

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Locate the form field named "Age"
            Field ageField = null;
            foreach (Page page in doc.Pages)
            {
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is Field field &&
                        (field.PartialName == "Age" || field.FullName.EndsWith("Age")))
                    {
                        ageField = field;
                        break;
                    }
                }
                if (ageField != null) break;
            }

            if (ageField != null)
            {
                // JavaScript that shows a warning when the entered value is less than 18
                string jsCode = "if (event.value < 18) { app.alert('You must be at least 18 years old.'); }";

                // Create the JavaScript action and attach it to the field
                JavascriptAction jsAction = new JavascriptAction(jsCode);
                ageField.ExecuteFieldJavaScript(jsAction);
            }
            else
            {
                Console.Error.WriteLine("Field \"Age\" not found in the document.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}