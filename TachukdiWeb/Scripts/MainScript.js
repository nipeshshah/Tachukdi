var baseUrl = 'http://localhost:51953/';
var apiurls = {
  AuthToken: "api/auth",
  Block: {
    City: "api/blocks/city",
    Cats: "api/blocks/city/",
    Blocks: "api/blocks/city/",//{cityid}/cat/{catid}",
    AddBlock: "api/blocks/submit"
  },
  Config: {
    Cats: "api/config/cats",
    City: "api/config/cites"
  },
  Point: {
    Total: "api/point/total",
    TopTen: "api/point/topten"
  },
  Refer: {
    MyId: "api/refer/myid",
    Referrals: "api/refer/all"
  },
  User: {
    Join: "api/user/join",
    ConfirmOPT: "api/user/otpconfirm",
    Login: "api/user/login",
    Profile: "api/user/profile",
    UpdateProfile: "api/user/updateprofile"
  }
};

var services =
{
  getServiceWithOutToken: function (url, callback, errorCallBack) {
    //alert(baseUrl + url);
    var settings = {
      "url": baseUrl + url,
      "method": "GET",
      "timeout": 0,
      error: function (err) {
        if (err.status == 401) {
          window.location.href = '/auth';
        }
        if (errorCallBack == undefined && toastr != undefined)
          toastr.error(err.responseJSON.message);
        else if (errorCallBack == undefined && toastr == undefined)
          alert(err.responseJSON.message);
        else
          errorCallBack(err);
      },
      success: callback
    };

    $.ajax(settings);
  },
  getServiceWithToken: function (url, callback, errorCallBack) {
    //alert(baseUrl + url);
    var settings = {
      "url": baseUrl + url,
      "method": "GET",
      "headers": {
        'authorization': `bearer ${loginconfiguration.token}`,
      },
      "timeout": 0,
      error: function (err) {
        if (err.status == 401) {
          window.location.href = '/auth';
        }
        if (errorCallBack == undefined && toastr != undefined)
          toastr.error(err.responseJSON.message);
        else if (errorCallBack == undefined && toastr == undefined)
          alert(err.responseJSON.message);
        else
          errorCallBack(err);
      },
      success: callback
    };

    $.ajax(settings);
  },
  postServiceWithToken: function (url, formData, callback, errorCallBack) {
    //alert(baseUrl + url);
    var settings = {
      "url": baseUrl + url,
      "method": "POST",
      //"headers": {
      //  'authorization': `bearer ${loginconfiguration.token}`,
      //},
      "timeout": 0,
      "processData": false,
      "contentType": false,
      "headers": {
          "Content-Type": "application/json"
      },
      "data": formData,
      error: function (err) {
        if (errorCallBack == undefined && toastr != undefined)
          toastr.error(err.responseJSON.message);
        else if (errorCallBack == undefined && toastr == undefined)
          alert(err.responseJSON.message);
        else
          errorCallBack(err);
      },
      success: callback
    };

    $.ajax(settings);
  }
};

//Page Functions

///// KO-OnsenUI """bindings"""
document.addEventListener('init', function (event) {
  var page = event.target;
  if (page.data && page.data.viewModel) {
    // Shortcut for ons-navigator
    ko.applyBindings(page.data.viewModel, page);
  } else {
    // Everything else by ID

    var viewModel = page.id.charAt(0).toUpperCase() + (page.id.split('-')[0] || '').slice(1) + 'ViewModel';
    console.log("Current View Model " + page.id + " & " + viewModel);
    if (window[viewModel]) {
      ko.applyBindings(new window[viewModel](), event.target);
    }
  }
});

// Settings page view model
function SettingsViewModel() {
  var self = this;

  self.info = 'More stuff';

  self.LoadPoints = function () {
    document.querySelector('ons-navigator')
      .pushPage('points.html', {
        data: { viewModel: new Points() }
      });
  };

  self.LoadReferrals = function () {
    document.querySelector('ons-navigator')
      .pushPage('referrals.html', {
        data: { viewModel: new Referral() }
      });
  };
}

function CityblockViewModel() {
  var self = this;
  self.cityBlocks = ko.observableArray([]);
  self.LoadDashboard = function () {
    services.getServiceWithOutToken(apiurls.Block.City, function (response) {
      response.forEach(function (item, index) {
        var m1 = new BlockCityModal();
        m1.CityId(item.CityId);
        m1.City(item.City);
        m1.BlockCount(item.BlockCount);
        self.cityBlocks.push(m1);
      });
    });
  }

  self.detailsItem = function () {
    document.querySelector('ons-navigator')
      .pushPage('Citycateblock.html', {
        data: { viewModel: new CitycateblockViewModel(this.CityId()) }
      });
  };

  self.LoadDashboard();
}

function CitycateblockViewModel(CityId) {
  var self = this;
  self.catBlocks = ko.observableArray([]);
  self.LoadDashboard = function () {
    services.getServiceWithOutToken(apiurls.Block.Cats + "/" + CityId, function (response) {
      response.forEach(function (item, index) {
        var m1 = new BlockCategoryModal();
        m1.CityId(item.CityId);
        m1.CatId(item.CatId);
        m1.Category(item.Category);
        m1.BlockCount(item.BlockCount);
        self.catBlocks.push(m1);
      });
    });
  };

  self.detailsItem = function () {
    document.querySelector('ons-navigator')
      .pushPage('Dashboardblock.html', {
        data: { viewModel: new DashboardBlock(this.CityId(), this.CatId()) }
      });
  };

  self.LoadDashboard();
}

function DashboardBlock(CityId, CatId) {
  var self = this;
  self.FullBlocks = ko.observableArray([]);
  self.LoadDashboard = function () {
    services.getServiceWithOutToken(apiurls.Block.Blocks + "/" + CityId + "/cat/" + CatId, function (response) {
      response.forEach(function (item, index) {

        var m1 = new BlockModal();
        m1.Content(item.Content);
        m1.MobileNo(item.MobileNo);
        m1.CreatedDate(item.CreatedDate);
        self.FullBlocks.push(m1);
      });
    });
  }
  self.LoadDashboard();
}

function LoginViewModel() {
  var self = this;
  self.Title = ko.observable('SOME CONTENT');
  self.UserName = ko.observable('asd');
  self.Password = ko.observable('qwe');
  self.Login = function () {
    services.postServiceWithToken(apiurls.User.Login + '?mobileno=' + self.UserName() + '&password=' + self.Password(), null, function (response) {
      debugger;
      ons.notification.toast('Login Called!', {
        timeout: 2000
      });
    });
  };
  self.Register = function () {
    ons.notification.toast('Register Called!', {
      timeout: 2000
    });
  };
}

function Points() {
  var self = this;
  self.TotalPoints = ko.observable(0);
  self.LastPointTransactions = ko.observableArray([]);
  self.GetTotalPoints = function () {
    services.getServiceWithOutToken(apiurls.Point.Total, function (response) {

      self.TotalPoints(response);
    });
  }

  self.GetPointTransactions = function () {
    services.getServiceWithOutToken(apiurls.Point.TopTen, function (response) {

      response.forEach(function (item, index) {
        var m1 = new PointTransactionModal();
        m1.TransactionDate(item.TransactionDate);
        m1.Description(item.Description);
        m1.Points(item.Points);
        self.LastPointTransactions.push(m1);
      });
    });
  }
  self.GetTotalPoints();
  self.GetPointTransactions();
}

function Referral() {
  var self = this;
  self.ReferralId = ko.observable('');
  self.ReferralIdMobile = ko.observable('');
  self.ReferralTransactions = ko.observableArray([]);
  self.GetReferralId = function () {
    services.getServiceWithOutToken(apiurls.Refer.MyId, function (response) {
      self.ReferralId(response.ReferralCode);
      self.ReferralIdMobile(response.OtherReferralCode);
    });
  };
  self.GetReferralTransactions = function () {
    services.getServiceWithOutToken(apiurls.Refer.Referrals, function (response) {
      response.forEach(function (item, index) {
        var m1 = new ReferralUserModal();
        m1.DisplayName(item.DisplayName);
        m1.JoinedOn(item.JoinedOn);
        m1.Mobile(item.Mobile);
        self.ReferralTransactions.push(m1);
      });
    });
  };
  self.GetReferralId();
  self.GetReferralTransactions();
}

function SubmitblockViewModel() {
  //debugger;
  var self = this;
  self.CatId = ko.observable(0);
  self.CityId = ko.observable(0);
  self.Content = ko.observable('XX');
  self.StartDate = ko.observable(moment().format('YYYY-MM-DD'));
  self.EndDate = ko.observable(moment().format('YYYY-MM-DD'));
  self.AllCities = ko.observableArray([]);
  self.AllCategories = ko.observableArray([]);
  self.BlockCost = ko.observable(1);
  self.StartDate.subscribe(function (newDate) {
    self.BlockCost(moment(self.EndDate()).diff(moment(newDate), 'days') + 1)
  }, self);
  self.EndDate.subscribe(function (newDate) {
    self.BlockCost(moment(newDate).diff(moment(self.StartDate()), 'days') + 1);
  }, self);
  self.SaveBlock = function () {
    debugger;
    var data = {};
    data['CatId'] = self.CatId();
    data['CityId'] = self.CityId();
    data['Content'] = self.Content();
    data['StartDate'] = self.StartDate();
    data['EndDate'] = self.EndDate();

    services.postServiceWithToken(apiurls.Block.AddBlock, JSON.stringify(data), function (response) {

    });

  }
  self.LoadCategories = function () {
    services.getServiceWithOutToken(apiurls.Config.Cats, function (response) {

      response.forEach(function (item, index) {
        var m1 = new CategoryModal();
        m1.CateId(item.CateId);
        m1.CateName(item.CateName);
        self.AllCategories.push(m1);
      });
    });
  };

  self.LoadCities = function () {
    services.getServiceWithOutToken(apiurls.Config.City, function (response) {
      response.forEach(function (item, index) {
        var m1 = new CityModal();
        m1.CityId(item.CityId);
        m1.CityName(item.CityName + " - " + item.State);
        self.AllCities.push(m1);
      });
    });
  };
  self.LoadCategories();
  self.LoadCities();
}