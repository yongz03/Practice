'use strict';
var searchApp = angular.module('searchApp', []);

searchApp.filter('siteSearch', function()
 {
    return function(input, query)
    {
        var sites = [];
        if(query && query.trim() && query.trim().length) {
            //split query string with comma and remove empty string
            var queries = query.trim().split(',').filter(function(i){return i});
            if (queries && queries.length) {
                angular.forEach(queries, function (query) {
                    angular.forEach(input, function (site) {
                        if (site.siteName.toLowerCase().indexOf(query.toLowerCase()) !== -1 && sites.indexOf(site) === -1) {
                            sites.push(site);
                        }
                        else {
                            var match = site.categories.filter(function (category) {
                                return category.description.toLowerCase().indexOf(query.toLowerCase()) !== -1;
                            });
                            if (match.length && sites.indexOf(site)===-1) {
                                sites.push(site);
                            }
                        }
                    });
                });
            }
        }
        return sites;
    }
 });

searchApp.controller("searchController", ['$scope', '$window', 'siteSearchFilter', function($scope, $window, siteSearchFilter)
{
    var sites = [
        {
            "id": 1,
            "siteName": "SurferMag",
            "siteUrl": "www.surfermag.com",
            "description": "This is the description for SurferMag",
            "categoryIds": [
                2
            ]
        },
        {
            "id": 2,
            "siteName": "Ebay",
            "siteUrl": "www.ebay.com.au",
            "description": "This is the description for ebay",
            "categoryIds": [
                1
            ]
        },
        {
            "id": 3,
            "siteName": "Robs UI Tips",
            "siteUrl": "www.awesomeui.com.au",
            "description": "This is the description for the best site in the world. It is the best:)",
            "categoryIds": [
                4, 3
            ]
        },
        {
            "id": 4,
            "siteName": "Table Tennis Tips - How to not come runners up",
            "siteUrl": "www.ttt.com",
            "description": "This is the description for Table Tennis Tips",
            "categoryIds": [
                1, 2, 3, 4
            ]
        }
    ];


    var categories = [
        {
            id: 1,
            description: "Arts & Entertainment"
        },
        {
            id: 2,
            description: "Automotive"
        },
        {
            id: 3,
            description: "Business"
        },
        {
            id: 4,
            description: "Careers"
        }
    ]

    $scope.search = {};
    $scope.search.extendedSites = [];

    angular.forEach(sites, function(site){
        var extendedSite={
            "id": site.id,
            "siteName": site.siteName,
            "siteUrl": site.siteUrl,
            "description": site.description,
            "categories": []
        };
        angular.forEach(categories, function(category){
            if(site.categoryIds.indexOf(category.id) !== -1)
            {
                extendedSite.categories.push({"id": category.id, description: category.description});
            }
        });
        $scope.search.extendedSites.push(extendedSite);
    });

    $scope.search.filteredSites = function()
    {
        return siteSearchFilter($scope.search.extendedSites, $scope.search.query);
    }

    $scope.redirectToUrl = function(url)
    {
        $window.open('http://' + url);
    }

    $scope.queryValidDirty = function()
    {
        return $scope.searchForm.searchQuery.$dirty && $scope.search.query && $scope.search.query.trim() !==',';
    }
}]);

