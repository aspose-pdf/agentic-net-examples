using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the source PDF files
        const string inputFolder  = @"C:\Pdf\Input";
        // Folder where the modified PDFs will be saved
        const string outputFolder = @"C:\Pdf\Output";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // JavaScript code to be attached to each rectangle (square) annotation
        const string jsCode = "app.alert('Rectangle annotation clicked');";

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(inputPath))
                {
                    // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];

                        // Iterate over all annotations on the current page
                        foreach (Annotation annotation in page.Annotations)
                        {
                            // Identify rectangle (square) annotations
                            if (annotation is SquareAnnotation squareAnno)
                            {
                                // NOTE:
                                // The current Aspose.Pdf version referenced does NOT expose an
                                // 'Action' property on SquareAnnotation, nor a JavaScriptAction class.
                                // Therefore a JavaScript alert cannot be attached directly.
                                // If a newer Aspose.Pdf version is used that provides these members,
                                // the following code would be appropriate (uncomment and adjust):
                                //
                                // var jsAction = new JavaScriptAction(jsCode);
                                // squareAnno.Action = jsAction;
                                //
                                // As a placeholder, we set the annotation's Title to indicate the intended action.
                                squareAnno.Title = "JavaScript alert intended";
                            }
                        }
                    }

                    // Save the modified document to the output folder
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {fileName} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }
    }
}