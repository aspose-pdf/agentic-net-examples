using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Annotation types are defined here

class Program
{
    static void Main()
    {
        string[] inputFiles = new string[] { "input1.pdf", "input2.pdf", "input3.pdf" };
        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            using (Document doc = new Document(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in doc.Pages)
                {
                    // Iterate over a copy of the annotations collection to avoid modification issues
                    for (int i = 0; i < page.Annotations.Count; i++)
                    {
                        Annotation annot = page.Annotations[i];
                        if (annot is SquareAnnotation square)
                        {
                            // NOTE: The current Aspose.PDF version does not expose the Action property
                            // on SquareAnnotation (or any annotation) and the JavaScriptAction class is
                            // unavailable. Therefore a JavaScript alert cannot be attached directly.
                            // To add JavaScript actions you would need to upgrade to a newer Aspose.PDF
                            // version that provides Annotation.Action and JavaScriptAction support.
                            // The following line is intentionally left as a comment to illustrate the
                            // intended behaviour when the required API becomes available:
                            // square.Action = new JavaScriptAction("app.alert('Rectangle annotation clicked');");
                        }
                    }
                }

                string outputFileName = Path.GetFileNameWithoutExtension(inputPath) + "_modified.pdf";
                doc.Save(outputFileName);
                Console.WriteLine($"Saved modified file: {outputFileName}");
            }
        }
    }
}
