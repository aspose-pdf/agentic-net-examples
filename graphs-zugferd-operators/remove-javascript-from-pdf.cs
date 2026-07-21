using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_clean.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // ==== Remove document‑level JavaScript actions ====
                if (doc.JavaScript != null)
                {
                    // Remove each script entry individually – JavaScriptCollection has no Clear() method.
                    var keys = doc.JavaScript.Keys.ToList(); // copy keys because we will modify the collection
                    foreach (var key in keys)
                    {
                        doc.JavaScript.Remove(key);
                    }
                }
                // Remove the OpenAction that could contain a JavaScript script
                doc.OpenAction = null;

                // ==== Remove page‑level JavaScript actions ====
                foreach (Page page in doc.Pages)
                {
                    // Page actions (OnOpen / OnClose)
                    page.Actions.OnOpen = null;
                    page.Actions.OnClose = null;

                    // Iterate backwards when deleting annotations
                    for (int i = page.Annotations.Count; i >= 1; i--)
                    {
                        Annotation ann = page.Annotations[i];

                        // Remove JavaScript actions from link annotations
                        if (ann is LinkAnnotation link && link.Action != null)
                        {
                            // The concrete type for a JavaScript action is JavaScriptAction.
                            // To avoid a direct reference to Aspose.Pdf.Actions, compare by name.
                            if (link.Action.GetType().Name == "JavaScriptAction")
                            {
                                link.Action = null;
                            }
                        }

                        // Remove embedded file attachments (optional security hardening)
                        if (ann is FileAttachmentAnnotation)
                        {
                            page.Annotations.Delete(i);
                        }
                    }
                }

                // Save the cleaned PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"JavaScript removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
