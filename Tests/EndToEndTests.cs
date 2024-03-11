using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;
using Sandbox;
using System.Text;

namespace Tests;

public class EndToEndTests
{
    [Fact]
    public async Task TheDisplayAndControlFlowOfTheAppIsCorrect()
    {
        var consoleOutput = GivenAConsole();

        var outputLines = await this.RunMainAndGetConsoleOutput(consoleOutput);

        outputLines.Should().HaveCountGreaterThanOrEqualTo(112);
        outputLines[0].Should().Be("Main Menu");
        outputLines[18].Should().Be("Main Menu");
        outputLines[76].Should().Be("Main Menu");
        outputLines[17].Should().Be("*User press a key to continue*");
        outputLines[49].Should().Be("*User press a key to continue*");
        outputLines[65].Should().Be("*User press a key to continue*");
        outputLines[4].Should().Be("    1. Choice 1");
        outputLines[38].Should().Be("    1. Sub Choice 1");
        outputLines[54].Should().Be("    1. Sub Choice 1");
        outputLines[46].Should().Be("Sub Menu First Choice");
        outputLines[62].Should().Contain("Sub Menu Choice with an async call");
        outputLines[92].Should().Be("This is a generated menu from a callback");
    }

    private async Task<string[]> RunMainAndGetConsoleOutput(StringBuilder consoleOutput)
    {
        await IntegrationTestProgram.Main(default);

        return consoleOutput.ToString().Split("\r\n");
    }

    private static StringBuilder GivenAConsole()
    {
        var consoleOutput = new StringBuilder();
        var consoleOutputWriter = new StringWriter(consoleOutput);
        Console.SetOut(consoleOutputWriter);
        Console.SetIn(GivenASequenceOfUserInput().Object);

        return consoleOutput;
    }

    private static Mock<TextReader> GivenASequenceOfUserInput()
    {
        var consoleInput = new Mock<TextReader>();
        var sequence = new MockSequence();
        var userInput = new string[] { "1", "2", "1", "2", "0", "3", "0", "0" };
        foreach (var response in userInput)
        {
            consoleInput.InSequence(sequence).Setup(x => x.ReadLine()).Returns(response);
        }

        return consoleInput;
    }
}