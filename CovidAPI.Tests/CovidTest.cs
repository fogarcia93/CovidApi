using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidAPI.Features;
using CovidAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace CovidAPI.Tests
{
    [TestClass]
    public class CovidTest
    {
        [TestMethod]
        public void ReturnSomeRegionsFromServices()
        {
            //Arrange
            var covidCasesAppService = new CovidCasesAppService();
          

            //Act
            var restResponse = Task.Run(() =>
            {
                return  covidCasesAppService.GetAllRegions();
            }).GetAwaiter().GetResult();

            //Assert
            restResponse.Count().Should().Be(216);

        }

        [TestMethod]
        public void ReturnDataInformationFromCountry()
        {
            //Arrange
            var covidCasesAppService = new CovidCasesAppService();


            //Act
            var restResponse = Task.Run(async () =>
            {
                return await covidCasesAppService.GetCovidCasesByRegionAndProvince("US","USA");
            }).GetAwaiter().GetResult();

            //Assert
            restResponse.Count().Should().Be(59);

        }

        [TestMethod]
        public void NotDataInformationReturnFromCountry()
        {
            //Arrange
            var covidCasesAppService = new CovidCasesAppService();


            //Act
            var restResponse = Task.Run(async () =>
            {
                return await covidCasesAppService.GetCovidCasesByRegionAndProvince("US", "USAA");
            }).GetAwaiter().GetResult();

            //Assert
            restResponse.Count().Should().Be(0);

        }


        [TestMethod]
        public void ReturnTopTenCasesByRegion()
        {
            //Arrange
            var covidCasesAppService = new CovidCasesAppService();


            //Act
            var restResponse = Task.Run(async () =>
            {
                return await covidCasesAppService.Top10CasesByRegion();
            }).GetAwaiter().GetResult();

            //Assert
            restResponse.Regions.Count().Should().Be(10);

        }

        [TestMethod]
        public void ReturnTopTenCasesByProvince()
        {
            //Arrange
            var covidCasesAppService = new CovidCasesAppService();


            //Act
            var restResponse = Task.Run(async () =>
            {
                return await covidCasesAppService.Top10CasesByProvince("China", "CHN");
            }).GetAwaiter().GetResult();

            //Assert
            restResponse.Count().Should().Be(10);

        }


        [TestMethod]
        public void ReturnNotDataFromTopTenCasesByProvince()
        {
            //Arrange
            var covidCasesAppService = new CovidCasesAppService();


            //Act
            var restResponse = Task.Run(async () =>
            {
                return await covidCasesAppService.Top10CasesByProvince("Taipei and environs", "TWN");
            }).GetAwaiter().GetResult();

            //Assert
            restResponse.Count().Should().Be(0);

        }

    }
}
