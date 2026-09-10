using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "signed_input.pdf";
        const string outputPath = "unsigned_output.pdf";
        const string password = ""; // leave empty if no password is required

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF. If a password is needed, pass it; otherwise use the overload without password.
            using (Document doc = string.IsNullOrEmpty(password)
                                   ? new Document(inputPath)
                                   : new Document(inputPath, password))
            {
                // If the document is encrypted, decrypt it first.
                // Decrypt() has no parameters; the password is supplied when opening the document.
                doc.Decrypt();

                // Enable signature sanitization (true by default). This removes all signature fields
                // and associated data when the document is saved.
                doc.EnableSignatureSanitization = true;

                // Optionally, explicitly remove any remaining SignatureField annotations.
                // This is a safety net in case sanitization does not cover all cases.
                foreach (Page page in doc.Pages)
                {
                    // Collect annotations to remove to avoid modifying the collection while iterating.
                    var toRemove = new List<Annotation>();
                    foreach (Annotation ann in page.Annotations)
                    {
                        if (ann is SignatureField)
                            toRemove.Add(ann);
                    }

                    foreach (Annotation ann in toRemove)
                        page.Annotations.Delete(ann);
                }

                // Save the unsigned PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Signature removed and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
