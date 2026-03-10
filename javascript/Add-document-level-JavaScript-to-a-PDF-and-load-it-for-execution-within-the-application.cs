using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, add document‑level JavaScript (executed on document open), and save
        using (Document doc = new Document(inputPath))
        {
            // The Document.JavaScript property is read‑only in recent Aspose.Pdf versions.
            // To attach JavaScript that runs when the document is opened, use the OpenAction property.
            doc.OpenAction = new JavascriptAction("app.alert('Document opened');");

            doc.Save(outputPath);
        }

        // Load the saved PDF and retrieve the JavaScript for execution within the app
        using (Document loadedDoc = new Document(outputPath))
        {
            // OpenAction holds the action that runs when the document is opened.
            if (loadedDoc.OpenAction is JavascriptAction jsAction)
            {
                string script = jsAction.Script;

                Console.WriteLine("Loaded document‑level JavaScript:");
                Console.WriteLine(script);

                // Here you could pass 'script' to a JavaScript engine (e.g., Jint) for execution.
            }
            else
            {
                Console.WriteLine("No document‑level JavaScript found.");
            }
        }
    }
}
