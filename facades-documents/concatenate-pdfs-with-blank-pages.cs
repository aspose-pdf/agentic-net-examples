using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Creates a temporary PDF file that contains a single blank page.
    static string CreateBlankPage()
    {
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        using (Document blankDoc = new Document())
        {
            // Add a blank page (default size A4).
            blankDoc.Pages.Add();
            blankDoc.Save(tempPath);
        }
        return tempPath;
    }

    // Concatenates the given PDF files, inserting a blank page between each consecutive document.
    static void ConcatenateWithBlankPages(string[] inputFiles, string outputFile)
    {
        if (inputFiles == null || inputFiles.Length == 0)
        {
            Console.Error.WriteLine("No input files specified.");
            return;
        }

        // Ensure all input files exist.
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Create a temporary blank‑page PDF.
        string blankPagePath = CreateBlankPage();

        // If there is only one file, just copy it to the output.
        if (inputFiles.Length == 1)
        {
            File.Copy(inputFiles[0], outputFile, true);
            File.Delete(blankPagePath);
            return;
        }

        // Start with the first file as the current intermediate result.
        string currentResult = inputFiles[0];
        string tempResult = null;

        // Iterate over the remaining files, concatenating two at a time with a blank page in between.
        for (int i = 1; i < inputFiles.Length; i++)
        {
            // Prepare a new temporary file for the next intermediate result.
            tempResult = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            // Use PdfFileEditor to concatenate currentResult, the next input file,
            // and the blank page file. The overload with four parameters inserts
            // blank pages where page counts differ, which satisfies the requirement.
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.Concatenate(currentResult, inputFiles[i], blankPagePath, tempResult);

            if (!success)
            {
                Console.Error.WriteLine($"Failed to concatenate '{currentResult}' and '{inputFiles[i]}'.");
                // Clean up temporary files before exiting.
                File.Delete(blankPagePath);
                if (File.Exists(tempResult)) File.Delete(tempResult);
                return;
            }

            // Delete the previous intermediate file if it was a temporary one.
            if (currentResult != inputFiles[0] && File.Exists(currentResult))
                File.Delete(currentResult);

            // The newly created file becomes the current result for the next iteration.
            currentResult = tempResult;
        }

        // Move the final intermediate file to the desired output location.
        File.Copy(currentResult, outputFile, true);

        // Clean up temporary files.
        if (File.Exists(currentResult) && currentResult != outputFile)
            File.Delete(currentResult);
        File.Delete(blankPagePath);
    }

    static void Main(string[] args)
    {
        // Example usage:
        // args[0] = output PDF path
        // args[1..n] = input PDF paths to be concatenated
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: ConcatenateWithBlankPages <output.pdf> <input1.pdf> [<input2.pdf> ...]");
            return;
        }

        string outputPath = args[0];
        string[] inputPaths = new string[args.Length - 1];
        Array.Copy(args, 1, inputPaths, 0, inputPaths.Length);

        try
        {
            ConcatenateWithBlankPages(inputPaths, outputPath);
            Console.WriteLine($"Successfully created '{outputPath}' with blank pages between documents.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}