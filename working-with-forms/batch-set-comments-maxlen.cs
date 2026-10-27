using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    // Maximum number of characters allowed in the "Comments" field
    private const int CommentsMaxLength = 100;

    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                string fileName = Path.GetFileName(pdfPath);
                string outPath = Path.Combine(outputFolder, fileName);

                // Load the PDF using the core Document API
                Document doc = new Document(pdfPath);

                // Try to locate a form field named "Comments"
                if (doc.Form != null && doc.Form["Comments"] is TextBoxField commentsField)
                {
                    // -----------------------------------------------------------------
                    // 1. Enforce the 100‑character limit on the *existing* value.
                    // -----------------------------------------------------------------
                    if (commentsField.Value != null)
                    {
                        string current = commentsField.Value.ToString();
                        if (current.Length > CommentsMaxLength)
                        {
                            commentsField.Value = current.Substring(0, CommentsMaxLength);
                        }
                    }

                    // -----------------------------------------------------------------
                    // 2. Add a client‑side JavaScript that prevents the user
                    //    from entering more than 100 characters when the PDF is opened
                    //    in a viewer that supports JavaScript (e.g., Adobe Acrobat).
                    // -----------------------------------------------------------------
                    string js = $"if (event.value.length > {CommentsMaxLength}) {{ event.value = event.value.substring(0, {CommentsMaxLength}); }}";
                    // Use a supported action property – OnModifyCharacter works for text fields.
                    commentsField.Actions.OnModifyCharacter = new JavascriptAction(js);
                }
                else
                {
                    Console.Error.WriteLine($"Field 'Comments' not found in '{fileName}'.");
                }

                // Save the modified PDF
                doc.Save(outPath);

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
