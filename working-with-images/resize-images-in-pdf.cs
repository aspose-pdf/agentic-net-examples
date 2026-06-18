using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "resized_output.pdf";
        const string configPath = "config.txt"; // file containing a single double value

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Config file not found: {configPath}");
            return;
        }

        // Read scaling factor from configuration file
        double scaleFactor;
        try
        {
            string text = File.ReadAllText(configPath).Trim();
            scaleFactor = double.Parse(text);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read scaling factor: {ex.Message}");
            return;
        }

        // Open PDF, apply scaling to each image, and save
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                OperatorCollection ops = page.Contents;

                // Walk through the content stream and look for image drawing operators (Do)
                // For each image we wrap the draw call with a scaling matrix.
                for (int opIdx = 0; opIdx < ops.Count; opIdx++)
                {
                    if (ops[opIdx] is Do doOp)
                    {
                        // Insert GSave before the image
                        ops.Insert(opIdx, new GSave());
                        opIdx++; // move past the inserted GSave

                        // Apply scaling matrix – uniform scaling on X and Y axes
                        // ConcatenateMatrix(a, b, c, d, e, f) corresponds to the PDF "cm" operator.
                        // For uniform scaling we set a = d = scaleFactor, others = 0.
                        ops.Insert(opIdx, new ConcatenateMatrix(scaleFactor, 0, 0, scaleFactor, 0, 0));
                        opIdx++; // move past the matrix operator

                        // The original Do operator (draw image) stays where it is.
                        // After the image we need to restore the graphics state.
                        ops.Insert(opIdx + 1, new GRestore());
                        // No need to adjust opIdx further because we want the loop to continue after the Do.
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Images resized with factor {scaleFactor} and saved to '{outputPdfPath}'.");
    }
}
