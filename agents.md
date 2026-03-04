# Aspose.PDF for .NET Examples — AI Agent Guide

This guide helps AI agents and code generation tools navigate and understand examples in this repository.

## Repository Overview

**AI-friendly repository** containing validated C# examples for Aspose.PDF for .NET API.

- **Total Categories**: 6
- **Examples per category**: 20-250 (varies by complexity)
- **Code Pattern**: Standalone `.cs` files (copy to `Program.cs`, run with dotnet)
- **Validation**: All examples tested against latest Aspose.PDF API

## Category Organization

Each category folder contains:
- **agents.md** — Category-specific guide with file listing
- **\*.cs files** — Standalone example files
- **Program.cs** — Template file (regenerated per example)

### Categories

#### Accessibility-and-Tagged-PDFs/
- Tagged PDF creation and structure elements
- Accessibility features (reading order, alternative text)
- **Guide**: [agents.md](./Accessibility-and-Tagged-PDFs/agents.md)

#### Compare-PDF/
- Document comparison (text, structure, visual)
- Difference detection and reporting
- **Guide**: [agents.md](./Compare-PDF/agents.md)

#### Conversion/
- Format conversion (PDF ↔ DOCX, HTML, SVG, XPS, etc.)
- Document format transformations
- **Guide**: [agents.md](./Conversion/agents.md)

#### Document/
- Document properties and metadata
- Watermarking and stamping
- Encryption and security
- **Guide**: [agents.md](./Document/agents.md)

#### Facades-AcroForms/
- Simplified AcroForms API
- Form field operations
- **Guide**: [agents.md](./Facades-AcroForms/agents.md)

#### Working-with-Annotation/
- Annotations (highlights, squares, circles, etc.)
- Comment and markup handling
- Annotation extraction
- **Guide**: [agents.md](./Working-with-Annotation/agents.md)

## Code Generation Rules

When generating Aspose.PDF examples, follow these patterns:

### 1. Project Setup
```csharp
// Assume modern .NET (net10.0 or net8.0 compatible)
// Aspose.PDF NuGet package pre-installed
// No additional setup code needed
```

### 2. File Conventions
- **Filenames**: Descriptive, task-based (e.g., `Add-Annotation-to-PDF.cs`)
- **Program.cs**: Standard entry point
- **Paths**: Relative paths OK (assume working dir = category folder)
- **I/O**: Files in current directory or `./Data/` subfolder

### 3. Code Structure
```csharp
using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        try
        {
            // Example code here
            Console.WriteLine("✓ Task completed successfully");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"✗ Error: {ex.Message}");
        }
    }
}
```

### 4. Important Patterns

**One-based Indexing**
```csharp
// PDF pages are 1-indexed, NOT 0-indexed
for (int i = 1; i <= doc.Pages.Count; i++)
{
    Page page = doc.Pages[i];  // Pages[1] is first page
    // ...
}
```

**Resource Cleanup (IDisposable)**
```csharp
// Always use 'using' for Document and other IDisposable types
using (Document pdfDoc = new Document("input.pdf"))
{
    // Work with document
    pdfDoc.Save("output.pdf");
}
```

**Error Handling**
```csharp
if (!File.Exists(inputPath))
{
    Console.Error.WriteLine($"File not found: {inputPath}");
    return;
}

try
{
    // Aspose.PDF operations
}
catch (FileNotFoundException)
{
    Console.Error.WriteLine("PDF file not found");
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Error: {ex.GetType().Name} – {ex.Message}");
}
```

### 5. Output Conventions
- **Success**: `Console.WriteLine("✓ ...")` or similar positive indicator
- **Errors**: `Console.Error.WriteLine("✗ ...")` to stderr
- **File output**: Save to `output.pdf` or descriptive name in current directory

## API Surface Coverage

This repository demonstrates:

| Feature | Categories | Status |
|---------|-----------|--------|
| Accessibility | Accessibility-and-Tagged-PDFs | ✓ |
| Annotations | Working-with-Annotation | ✓ |
| Comparison | Compare-PDF | ✓ |
| AcroForms | Facades-AcroForms | ✓ |
| Conversion | Conversion | ✓ |
| Document Ops | Document | ✓ |

## Common Mistakes to Avoid

1. **Using 0-based indexing** — Aspose.PDF uses 1-based indexing for Pages
   ```csharp
   // ✗ WRONG
   Page firstPage = doc.Pages[0];
   
   // ✓ CORRECT
   Page firstPage = doc.Pages[1];
   ```

2. **Not disposing Document** — Always use `using` blocks
   ```csharp
   // ✓ CORRECT
   using (Document doc = new Document("file.pdf"))
   {
       // ...
   }
   ```

3. **Assuming internal APIs are stable** — Use public APIs only
   ```csharp
   // ✗ Avoid internal namespaces
   using Aspose.Pdf.Internal;
   
   // ✓ Use public APIs
   using Aspose.Pdf;
   using Aspose.Pdf.Facades;
   ```

4. **Missing file existence checks**
   ```csharp
   // ✓ Always check before using
   if (!File.Exists(path)) {
       throw new FileNotFoundException(path);
   }
   ```

5. **Not handling exceptions** — Wrap operations in try-catch
   ```csharp
   try {
       // Operation
   } catch (Exception ex) {
       Console.Error.WriteLine($"Error: {ex.Message}");
   }
   ```

## Example File Naming

Files follow this pattern: `Feature-Description-Details-Format.cs`

Examples:
- `Add-Annotation-to-PDF-with-Reply-Chain.cs`
- `Convert-PDF-to-DOCX-with-Font-Substitution.cs`
- `Extract-Text-from-PDF-with-Formatting-Preservation.cs`
- `Merge-Multiple-PDFs-in-Batch-and-Save-as-XPS.cs`
- `Create-AcroForm-with-Validation-Rules.cs`

## Best Practices

### For Code Generation Tools
1. **Verify one-based indexing** in all example code
2. **Test with latest Aspose.PDF version** before release
3. **Include proper error handling** (try-catch, file checks)
4. **Use descriptive variable names** (avoid `x`, `p`, `doc1`)
5. **Add comments** for complex operations
6. **Keep examples focused** — one primary task per file

### For AI Agents
1. **Reference category-specific agents.md** for patterns
2. **Copy file listing from category agents.md** to understand available examples
3. **Check API Coverage table** to understand what's implemented
4. **Follow Code Generation Rules** for consistency
5. **Test generated code** with actual Aspose.PDF package before committing

## Working with This Repository

### Finding Examples
1. Identify feature from API documentation
2. Find category folder matching the feature
3. Review `agents.md` in that category
4. Browse available `.cs` files for similar patterns
5. Use file listing to locate exact example

### Running Examples
```bash
cd <CategoryFolder>
# Copy example file to Program.cs
cp "Example-Name.cs" Program.cs
# Run with dotnet
dotnet run
```

### Creating New Examples
1. Create new `.cs` file in appropriate category
2. Follow code structure and patterns above
3. Include comprehensive error handling
4. Test with actual Aspose.PDF package
5. Update category `agents.md` with file listing

## Support & Resources

- **Documentation**: [docs.aspose.com/pdf/net/](https://docs.aspose.com/pdf/net/)
- **API Reference**: [reference.aspose.com/pdf/net/](https://reference.aspose.com/pdf/net/)
- **Forum**: [forum.aspose.com/c/pdf/10](https://forum.aspose.com/c/pdf/10)
- **GitHub**: [This Repository](https://github.com/aspose-pdf/agentic-net-examples)

## Repository Stats

- **Last Updated**: 2026-03-04
- **Examples**: 1000+ validated examples
- **Categories**: 7 major feature categories
- **Version**: Aspose.PDF 26.2.0+
- **Validation**: All examples tested and working


