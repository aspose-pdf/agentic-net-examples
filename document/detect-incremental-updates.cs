using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            bool hasIncremental = doc.HasIncrementalUpdate();
            string originalId = doc.Id.Original;
            string modifiedId = doc.Id.Modified;
            bool isModified = !originalId.Equals(modifiedId, StringComparison.Ordinal);

            Console.WriteLine($"Has incremental updates: {hasIncremental}");
            Console.WriteLine($"Original ID: {originalId}");
            Console.WriteLine($"Modified ID: {modifiedId}");
            Console.WriteLine($"Document modified after creation: {isModified}");
        }
    }
}
