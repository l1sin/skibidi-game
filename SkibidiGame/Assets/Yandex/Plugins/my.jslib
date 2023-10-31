mergeInto(LibraryManager.library, {

  GetLanguage: function () {
    var lang = ysdk.environment.i18n.lang;

    var bufferSize = lengthBytesUTF8(lang) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(lang, buffer, bufferSize);
    return buffer;
  },

  Rate: function () 
  {
    ysdk.feedback.requestReview()
    .then(({ feedbackSent }) => {
      console.log(feedbackSent);
    })
  },

  WatchAd: function () {
    ysdk.adv.showRewardedVideo({
      callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          myGameInstance.SendMessage('MainMenuController', 'GetAdReward');
          console.log('Rewarded 500 money');
        },
        onClose: () => {
          console.log('Video ad closed.');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
      }
    })
  },

  SaveExtern: function (data) {
    if (player.getMode() === 'lite')
    {
      console.log("Saving. No auth. No cloud save, only local.");
    }
    else
    {
      var dataString = UTF8ToString(data);
      var myobj = JSON.parse(dataString);
      player.setData(myobj, true);
      console.log("Saving. Auth. Save to cloud.");
    }
  },

  LoadExtern: function () {
    if (player.getMode() === 'lite')
    {
      console.log("Loading. No auth. Use local save.");
      myGameInstance.SendMessage('SaveManager', 'LoadDataLocal');
    }
    else
    {
      player.getData().then(_data => {
        console.log("Loading. Auth. Use cloud save.");
        const myJSON = JSON.stringify(_data);
        myGameInstance.SendMessage('SaveManager', 'LoadDataCloud', myJSON);
      });
    }
  },

  FullScreenAd: function () {
    ysdk.adv.showFullscreenAdv({
      callbacks: {
        onClose: function(wasShown) {
          // some action after close
        },
        onError: function(error) {
          // some action on error
        }
      }
    })
  },

  CallPurchaseMenu: function () {

  },

  GetPrice: function (id) {
    var item = gameShop[id]
    var price = item.priceValue;
    var bufferSize = lengthBytesUTF8(price) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(price, buffer, bufferSize);
    return buffer;
  },

  GetYanIcon: function () {
    var url = gameShop[0].getPriceCurrencyImage('medium')
    myGameInstance.SendMessage("MainMenuController", "SetYanTexture", url); 
  },

  CallRate: function()
  {
    ysdk.feedback.canReview()
    .then(({ value, reason }) => {
      if (value) 
      {
        myGameInstance.SendMessage("MainMenuController", "ShowRateWindow");
      } 
      else 
      {
        console.log(reason)
      }
    })
  }
});