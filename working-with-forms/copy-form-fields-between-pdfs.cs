using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string destinationPdfPath = "destination.pdf";
        const string outputPdfPath = "merged_forms.pdf";

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(destinationPdfPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPdfPath}");
            return;
        }

        try
        {
            // Load source and destination documents
            using (Document srcDoc = new Document(sourcePdfPath))
            using (Document dstDoc = new Document(destinationPdfPath))
            {
                // Iterate over each form field in the source document
                foreach (Field srcField in srcDoc.Form.Fields)
                {
                    // Determine the page number where the source field resides
                    int srcPageNumber = 0;
                    for (int i = 1; i <= srcDoc.Pages.Count; i++)
                    {
                        if (srcDoc.Pages[i].Annotations.Contains(srcField))
                        {
                            srcPageNumber = i;
                            break;
                        }
                    }

                    // If the field is not attached to any page, skip it
                    if (srcPageNumber == 0)
                        continue;

                    // Add (copy) the field to the destination document on the same page number
                    // The Add method creates a copy of the field in the target document.
                    dstDoc.Form.Add(srcField, srcPageNumber);
                }

                // Save the resulting document with merged form fields
                dstDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Form fields copied successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}