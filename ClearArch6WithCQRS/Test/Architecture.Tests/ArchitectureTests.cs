using NetArchTest.Rules;

namespace Architecture.Tests;

public class ArchitectureTests
{
    private const string DomainNameSpace = "Domain";
    private const string ApplicationNameSpace = "Application";
    private const string InfraNameSpace = "Infrastructure";
    private const string PresentationNameSpace = "Presentation";
    private const string WebNameSpace = "Web";

    [Fact]
    public void Domain_ShouldNot_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Domain.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            ApplicationNameSpace,
            InfraNameSpace,
            PresentationNameSpace,
            WebNameSpace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        //Assert
        Assert.True(testResult.IsSuccessful);
    }

    [Fact]
    public void Application_ShouldNot_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Application.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            InfraNameSpace,
            PresentationNameSpace,
            WebNameSpace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        //Assert
        Assert.True(testResult.IsSuccessful);
    }

    [Fact]
    public void Infrastructure_ShouldNot_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Application.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            PresentationNameSpace,
            WebNameSpace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        //Assert
        Assert.True(testResult.IsSuccessful);
    }

    [Fact]
    public void Presentation_ShouldNot_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Application.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            InfraNameSpace,
            WebNameSpace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        //Assert
       Assert.True(testResult.IsSuccessful);
    }

    [Fact]
    public void Handlers_Should_Have_DependencyOnDomain()
    {
        // Arrange
        var assembly = typeof(Application.AssemblyReference).Assembly;
        
        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .HaveDependencyOn(DomainNameSpace)
            .GetResult();

        //Assert
        Assert.True(testResult.IsSuccessful);
    }
    
    
    [Fact]
    public void Controllers_Should_Have_DependencyOnMediatR()
    {
        // Arrange
        var assembly = typeof(Presentation.AssemblyReference).Assembly;
        
        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Controller")
            .Should()
            .HaveDependencyOn("MediatR")
            .GetResult();

        //Assert
        Assert.True(testResult.IsSuccessful);
    }
}