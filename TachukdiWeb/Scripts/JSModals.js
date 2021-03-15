//Modals

function BlockCityModal() {
  var self = this;
  self.CityId = ko.observable(0);
  self.City = ko.observable('');
  self.BlockCount = ko.observable(0);
  return self;
}

function BlockCategoryModal() {
  var self = this;
  self.CityId = ko.observable(0);
  self.CatId = ko.observable(0);
  self.BlockCount = ko.observable(0);
  self.Category = ko.observable('');
  return self;
}

function BlockModal() {
  var self = this;
  self.IsLogin = ko.observable(false);
  self.Content = ko.observable('');
  self.MobileNo = ko.observable('');
  self.CreatedDate = ko.observable('');
  return self;
}

function PointTransactionModal() {
  var self = this;
  self.TransactionDate = ko.observable('');
  self.Description = ko.observable('');
  self.Points = ko.observable(0);
  return self;
}

function ReferralUserModal() {
  var self = this;
  self.DisplayName = ko.observable('');
  self.JoinedOn = ko.observable('');
  self.Mobile = ko.observable('');
  return self;
}

function CategoryModal() {
  var self = this;
  self.CateId = ko.observable(0);
  self.CateName = ko.observable('');
}

function CityModal() {
  var self = this;
  self.CityId = ko.observable(0);
  self.CityName = ko.observable('');
  self.State = ko.observable('');
}
