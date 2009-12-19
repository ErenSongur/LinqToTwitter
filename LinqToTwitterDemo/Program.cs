﻿using System;
using System.Linq;

using LinqToTwitter;
using System.Net;
using System.Diagnostics;
using System.Web;
using System.Collections.Specialized;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;
using System.Configuration;

namespace LinqToTwitterDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // For testing globalization, uncomment and change 
            // locale to a locale that is not yours
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-PT");

            //
            // get user credentials and instantiate TwitterContext
            //
            ITwitterAuthorization auth;

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["twitterConsumerKey"]) || string.IsNullOrEmpty(ConfigurationManager.AppSettings["twitterConsumerSecret"]))
            {
                Console.WriteLine("Skipping OAuth authorization demo because twitterConsumerKey and/or twitterConsumerSecret are not set in your .config file.");
                Console.WriteLine("Using username/password authorization instead.");

                // For username/password authorization demo...
                auth = new UsernamePasswordAuthorization(Utilities.GetConsoleHWnd());
            }
            else
            {
                Console.WriteLine("Discovered Twitter OAuth consumer key in .config file.  Using OAuth authorization.");

                // For OAuth authorization demo...
                auth = new DesktopOAuthAuthorization();

                // If you wanted to pass the consumer key and secret in programmatically, you could do so as shown here.
                // Otherwise this information is pulled out of your .config file.
                ////var desktopAuth = (DesktopOAuthAuthorization)auth;
                ////desktopAuth.ConsumerKey = "some key";
                ////desktopAuth.ConsumerSecret = "some secret";
            }

            // TwitterContext is similar to DataContext (LINQ to SQL) or ObjectContext (LINQ to Entities)

            // For Twitter
            using (var twitterCtx = new TwitterContext(auth, "https://api.twitter.com/1/", "http://search.twitter.com/"))
            {

                // For JTweeter (Laconica)
                //var twitterCtx = new TwitterContext(passwordAuth, "http://jtweeter.com/api/", "http://search.twitter.com/");

                // For Identi.ca (Laconica)
                //var twitterCtx = new TwitterContext(passwordAuth, "http://identi.ca/api/", "http://search.twitter.com/");

                // If we're using OAuth, we need to configure it with the ConsumerKey etc. from the user.
                if (twitterCtx.AuthorizedClient is OAuthAuthorization)
                {
                    InitializeOAuthConsumerStrings(twitterCtx);
                }

                // Whatever authorization module we selected... sign on now.  
                // See the bottom of the method for sign-off procedures.
                try
                {
                    auth.SignOn();
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Login canceled. Demo exiting.");
                    return;
                }

                //
                // status tweets
                //

                UpdateStatusDemo(twitterCtx);
                //SingleStatusQueryDemo(twitterCtx);
                //UpdateStatusWithReplyDemo(twitterCtx);
                //DestroyStatusDemo(twitterCtx);
                //UserStatusByNameQueryDemo(twitterCtx);
                //UserStatusQueryDemo(twitterCtx);
                //FirstStatusQueryDemo(twitterCtx);
                //PublicStatusQueryDemo(twitterCtx);
                //PublicStatusFilteredQueryDemo(twitterCtx);
                //MentionsStatusQueryDemo(twitterCtx);
                //FriendStatusQueryDemo(twitterCtx);
                //HomeStatusQueryDemo(twitterCtx);
                //RetweetDemo(twitterCtx);
                //RetweetsQueryDemo(twitterCtx);
                //RetweetedByMeStatusQueryDemo(twitterCtx);
                //RetweetedToMeStatusQueryDemo(twitterCtx);
                //RetweetsOfMeStatusQueryDemo(twitterCtx);
                //GetAllTweetsAndRetweetsDemo(twitterCtx);

                //
                // user tweets
                //

                //UserShowWithIDQueryDemo(twitterCtx);
                //UserShowWithScreenNameQueryDemo(twitterCtx);
                //UserFriendsQueryDemo(twitterCtx);
                //UserFriendsWithCursorQueryDemo(twitterCtx);
                //UserFollowersQueryDemo(twitterCtx);
                //UserFollowersWithCursorsQueryDemo(twitterCtx);
                //GetAllFollowersQueryDemo(twitterCtx);
                //VerifiedAndGeoEnabledDemo(twitterCtx);

                //
                // direct messages
                //

                //DirectMessageSentByQueryDemo(twitterCtx);
                //DirectMessageSentToQueryDemo(twitterCtx);
                //NewDirectMessageDemo(twitterCtx);
                //DestroyDirectMessageDemo(twitterCtx);

                //
                // friendship
                //

                //CreateFriendshipFollowDemo(twitterCtx);
                //FriendshipExistsDemo(twitterCtx);
                //DestroyFriendshipDemo(twitterCtx);
                //CreateFriendshipNoDeviceUpdatesDemo(twitterCtx);

                //
                // SocialGraph
                //

                //ShowFriendsDemo(twitterCtx);
                //ShowFriendsWithCursorDemo(twitterCtx);
                //ShowFollowersDemo(twitterCtx);
                //ShowFollowersWithCursorDemo(twitterCtx);

                //
                // Search
                //

                //SearchTwitterDemo(twitterCtx);
                //SearchTwitterSource(twitterCtx);
                //ExceedSearchRateLimitDemo(twitterCtx);

                //
                // Favorites
                //

                //FavoritesQueryDemo(twitterCtx);
                //CreateFavoriteDemo(twitterCtx);
                //DestroyFavoriteDemo(twitterCtx);

                //
                // Notifications
                //

                //EnableNotificationsDemo(twitterCtx);
                //EnableNotificationsWithScreenNameDemo(twitterCtx);
                //EnableNotificationsWithUserIDDemo(twitterCtx);
                //DisableNotificationsDemo(twitterCtx);

                //
                // Blocks
                //

                //CreateBlock(twitterCtx);
                //DestroyBlock(twitterCtx);
                //BlockExistsDemo(twitterCtx);
                //BlockIDsDemo(twitterCtx);
                //BlockBlockingDemo(twitterCtx);

                //
                // Help
                //

                //PerformHelpTest(twitterCtx);

                //
                // Account
                //

                //VerifyAccountCredentials(twitterCtx);
                //ViewRateLimitStatus(twitterCtx);
                //ViewRateLimitResponseHeadersDemo(twitterCtx);
                //EndSession(twitterCtx);
                //UpdateDeliveryDevice(twitterCtx);
                //UpdateAccountColors(twitterCtx);
                //UpdateAccountImage(twitterCtx);
                //UpdateAccountBackgroundImage(twitterCtx);
                //UpdateAccountBackgroundImageBytes(twitterCtx);
                //UpdateAccountBackgroundImageAndTileDemo(twitterCtx);
                //UpdateAccountBackgroundImageWithProgressUpdates(twitterCtx);
                //UpdateAccountInfoDemo(twitterCtx);

                //
                // Trends
                //

                //SearchTrendsDemo(twitterCtx);
                //SearchCurrentTrendsDemo(twitterCtx);
                //SearchDailyTrendsDemo(twitterCtx);
                //SearchWeeklyTrendsDemo(twitterCtx);

                //
                // Error Handling Demos
                //

                //HandleQueryExceptionDemo(twitterCtx);
                //HandleSideEffectExceptionDemo(twitterCtx);
                //HandleSideEffectWithFilePostExceptionDemo(twitterCtx);
                //HandleTimeoutErrors(twitterCtx);

                //
                // Oauth Demos
                //

                //HandleOAuthQueryDemo(twitterCtx);
                //HandleOAuthSideEffectDemo(twitterCtx);
                //HandleOAuthFilePostDemo(twitterCtx);
                //HandleOAuthReadOnlyQueryDemo(twitterCtx);
                //HandleOAuthSideEffectReadOnlyDemo(twitterCtx);
                //HandleOAuthUpdateAccountBackgroundImageWithProgressUpdatesDemo(twitterCtx);
                //HandleOAuthRequestResponseDetailsDemo(twitterCtx);
                //OAuthForceLoginDemo(twitterCtx);

                //
                // Saved Search Demos
                //

                //QuerySavedSearchesDemo(twitterCtx);
                //QuerySavedSearchesShowDemo(twitterCtx);
                //CreateSavedSearchDemo(twitterCtx);
                //DestroySavedSearchDemo(twitterCtx);

                //
                // Report Spam Demos
                //

                //ReportSpamDemo(twitterCtx);

                //
                // List Demos
                //

                //GetListsDemo(twitterCtx);
                //IsListSubscribedDemo(twitterCtx);
                //GetListSubscribersDemo(twitterCtx);
                //IsListMemberDemo(twitterCtx);
                //GetListMembersDemo(twitterCtx);
                //GetListSubscriptionsDemo(twitterCtx);
                //GetListMembershipsDemo(twitterCtx);
                //GetListStatusesDemo(twitterCtx);
                //GetListDemo(twitterCtx);
                //CreateListDemo(twitterCtx);
                //UpdateListDemo(twitterCtx);
                //DeleteListDemo(twitterCtx);
                //AddMemberToListDemo(twitterCtx);
                //DeleteMemberFromListDemo(twitterCtx);
                //SubscribeToListDemo(twitterCtx);
                //UnsubscribeFromListDemo(twitterCtx);
                //ListSortDemo(twitterCtx);

                //
                // Sign-off, including optional clearing of cached credentials.
                //

                //auth.SignOff();
                //auth.ClearCachedCredentials();
            }

            Console.WriteLine("Press any key to end this demo.");
            Console.ReadKey();
        }

        #region List Demos

        /// <summary>
        /// Shows how to get a list and sort it
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void ListSortDemo(TwitterContext twitterCtx)
        {
            var lists =
                from list in twitterCtx.List
                where list.Type == ListType.Lists &&
                      list.ScreenName == "LinqToTweeter"
                orderby list.Name
                select list;

            foreach (var list in lists)
            {
                Console.WriteLine("List Name: {0}, Description: {1}",
                    list.Name, list.Description);
            }
        }

        /// <summary>
        /// Shows how a user can unsubscribe from a list
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void UnsubscribeFromListDemo(TwitterContext twitterCtx)
        {
            List list = twitterCtx.UnsubscribeFromList("LinqToTweeter", "linq");

            Console.WriteLine("List Name: {0}, Description: {1}",
                list.Name, list.Description);
        }

        /// <summary>
        /// Shows how a user can subscribe to a list
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void SubscribeToListDemo(TwitterContext twitterCtx)
        {
            List list = twitterCtx.SubscribeToList("LinqToTweeter", "linq");

            Console.WriteLine("List Name: {0}, Description: {1}",
                list.Name, list.Description);
        }

        /// <summary>
        /// Shows how to remove a member from a list
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void DeleteMemberFromListDemo(TwitterContext twitterCtx)
        {
            List list = twitterCtx.DeleteMemberFromList("LinqToTweeter", "linq", "15411837");

            Console.WriteLine("List Name: {0}, Description: {1}",
                list.Name, list.Description);
        }

        /// <summary>
        /// Shows how to add a member to a list
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void AddMemberToListDemo(TwitterContext twitterCtx)
        {
            List list = twitterCtx.AddMemberToList("LinqToTweeter", "linq", "15411837");

            Console.WriteLine("List Name: {0}, Description: {1}",
                list.Name, list.Description);
        }

        /// <summary>
        /// Shows how to delete a list
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void DeleteListDemo(TwitterContext twitterCtx)
        {
            List list = twitterCtx.DeleteList("LinqToTweeter", "test2");

            Console.WriteLine("List Name: {0}, Description: {1}",
                list.Name, list.Description);
        }

        /// <summary>
        /// Shows how to modify an existing list
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void UpdateListDemo(TwitterContext twitterCtx)
        {
            List list = twitterCtx.UpdateList("LinqToTweeter", "test", "test2", "public", "This is a test2");

            Console.WriteLine("List Name: {0}, Description: {1}",
                list.Name, list.Description);
        }

        /// <summary>
        /// Shows how to create a new list
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void CreateListDemo(TwitterContext twitterCtx)
        {
            List list = twitterCtx.CreateList("LinqToTweeter", "test", "public", "This is a test");

            Console.WriteLine("List Name: {0}, Description: {1}",
                list.Name, list.Description);
        }

        /// <summary>
        /// Shows how to get information for a specific list
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void GetListDemo(TwitterContext twitterCtx)
        {
            var requestedList =
                (from list in twitterCtx.List
                 where list.Type == ListType.List &&
                       list.ScreenName == "LinqToTweeter" && // user to get memberships for
                       list.ID == "mvc" // ID of list
                 select list)
                 .FirstOrDefault();

            Console.WriteLine("List Name: {0}, Description: {1}",
                requestedList.Name, requestedList.Description);
        }

        /// <summary>
        /// Gets a list of statuses for specified list
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void GetListStatusesDemo(TwitterContext twitterCtx)
        {
            var statusList =
                (from list in twitterCtx.List
                 where list.Type == ListType.Statuses &&
                       list.ScreenName == "LinqToTweeter" &&
                       list.ListID == 3897016 // ID of list to get statuses for
                 select list)
                 .First();

            foreach (var status in statusList.Statuses)
            {
                Console.WriteLine("User: {0}, Status: {1}",
                    status.User.Name, status.Text);
            }
        }

        /// <summary>
        /// Gets a list of memberships for a user
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void GetListMembershipsDemo(TwitterContext twitterCtx)
        {
            var lists =
                from list in twitterCtx.List
                where list.Type == ListType.Memberships &&
                      list.ScreenName == "JoeMayo" // user to get memberships for
                select list;

            foreach (var list in lists)
            {
                Console.WriteLine("List Name: {0}, Description: {1}",
                    list.Name, list.Description);
            }
        }

        /// <summary>
        /// Gets a list of subscriptions for a user
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void GetListSubscriptionsDemo(TwitterContext twitterCtx)
        {
            var lists =
                from list in twitterCtx.List
                where list.Type == ListType.Subscriptions &&
                      list.ScreenName == "JoeMayo" // user to get subscriptions for
                select list;

            foreach (var list in lists)
            {
                Console.WriteLine("List Name: {0}, Description: {1}",
                    list.Name, list.Description);
            }
        }

        /// <summary>
        /// Gets a list of members of a list
        /// </summary>
        /// <param name="twitterCtx">Twitter Context</param>
        private static void GetListMembersDemo(TwitterContext twitterCtx)
        {
            var lists =
                (from list in twitterCtx.List
                 where list.Type == ListType.Members &&
                       list.ScreenName == "LinqToTweeter" &&
                       list.ListID == 3897006 // ID of list
                 select list)
                 .First();

            foreach (var user in lists.Users)
            {
                Console.WriteLine("Member: " + user.Name);
            }
        }

        /// <summary>
        /// Sees if user is a member of specified list
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void IsListMemberDemo(TwitterContext twitterCtx)
        {
            try
            {
                var subscribedList =
                   (from list in twitterCtx.List
                    where list.Type == ListType.IsMember &&
                         list.ScreenName == "LinqToTweeter" &&
                         list.ID == "15411837" && // ID of user
                         list.ListID == 3897006 // ID of list
                    select list)
                    .FirstOrDefault();

                // list will have only one user matching ID in query
                var user = subscribedList.Users.First();

                Console.WriteLine("User: {0} is a member of List: {1}",
                    user.Name, subscribedList.ListID);
            }
            // whenever user is not subscribed to the specified list, Twitter
            // returns an HTTP 404, Not Found, response, which results in a
            // .NET exception.  LINQ to Twitter intercepts the HTTP exception
            // and wraps it in a TwitterQueryResponse where you can read the
            // error message from Twitter via the Response property, shown below.
            catch (TwitterQueryException tqe)
            {
                Console.WriteLine(
                    "User is not a member of List. Response from Twitter: " +
                    tqe.Response.Error);
            }
        }

        /// <summary>
        /// Gets a list of subscribers for specified list
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void GetListSubscribersDemo(TwitterContext twitterCtx)
        {
            var lists =
                (from list in twitterCtx.List
                 where list.Type == ListType.Subscribers &&
                       list.ScreenName == "LinqToTweeter" &&
                       list.ListID == 3897016 // ID of list
                 select list)
                 .First();

            foreach (var user in lists.Users)
            {
                Console.WriteLine("Subscriber: " + user.Name);
            }
        }

        /// <summary>
        /// Gets lists that user created
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void GetListsDemo(TwitterContext twitterCtx)
        {
            var lists =
                from list in twitterCtx.List
                where list.Type == ListType.Lists &&
                      list.ScreenName == "LinqToTweeter"
                select list;

            foreach (var list in lists)
            {
                Console.WriteLine("List Name: {0}, Description: {1}",
                    list.Name, list.Description);
            }
        }

        /// <summary>
        /// Sees if user is subscribed to specified list
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void IsListSubscribedDemo(TwitterContext twitterCtx)
        {
            try
            {
                var subscribedList =
                   (from list in twitterCtx.List
                    where list.Type == ListType.IsSubscribed &&
                         list.ScreenName == "LinqToTweeter" &&
                         list.ID == "15411837" && // ID of user
                         list.ListID == 3897016 // ID of list
                    select list)
                    .FirstOrDefault();

                // list will have only one user matching ID in query
                var user = subscribedList.Users.First();

                Console.WriteLine("User: {0} is subscribed to List: {1}",
                    user.Name, subscribedList.ListID);
            }
            // whenever user is not subscribed to the specified list, Twitter
            // returns an HTTP 404, Not Found, response, which results in a
            // .NET exception.  LINQ to Twitter intercepts the HTTP exception
            // and wraps it in a TwitterQueryResponse where you can read the
            // error message from Twitter via the Response property, shown below.
            catch (TwitterQueryException tqe)
            {
                Console.WriteLine(
                    "User is not subscribed to List. Response from Twitter: " + 
                    tqe.Response.Error);
            }
        }

        #endregion

        #region Report Spam Demos

        /// <summary>
        /// Shows multiple ways to report spammers
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void ReportSpamDemo(TwitterContext twitterCtx)
        {
            var spammer = twitterCtx.ReportSpam(null, null, "Greer_105");
            Console.WriteLine("Spammer \"{0}\" Zapped! He he :)", spammer.Name);

            // after the first one, subsequent calls won't report spam to Twitter
            // but hopefully you can see my enthusiasm for this API;
            // besides, a couple extra examples might be helpful - Joe

            spammer = twitterCtx.ReportSpam("84705854", null, null);
            Console.WriteLine("Spammer \"{0}\" Zapped again! Ha Ha :)", spammer.Name);

            spammer = twitterCtx.ReportSpam(null, "84705854", null);
            Console.WriteLine("Spammer \"{0}\" is so gone! ... and don't come back! :)", spammer.Name);
        }

        #endregion

        #region Saved Search Demos

        /// <summary>
        /// Shows how to delete a saved search
        /// </summary>
        /// <remarks>
        /// Trying to delete a saved search that doesn't exist results
        /// in a TwitterQueryException with HTTP Status 404 (Not Found)
        /// </remarks>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void DestroySavedSearchDemo(TwitterContext twitterCtx)
        {
            var savedSearch = twitterCtx.DestroySavedSearch(329820);

            Console.WriteLine("ID: {0}, Search: {1}", savedSearch.ID, savedSearch.Name);
        }

        /// <summary>
        /// shows how to create a Saved Search
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void CreateSavedSearchDemo(TwitterContext twitterCtx)
        {
            var savedSearch = twitterCtx.CreateSavedSearch("#csharp");

            Console.WriteLine("ID: {0}, Search: {1}", savedSearch.ID, savedSearch.Name);
        }

        /// <summary>
        /// shows how to retrieve a single search
        /// </summary>
        /// <remarks>
        /// Trying to delete a saved search that doesn't exist results
        /// in a TwitterQueryException with HTTP Status 404 (Not Found)
        /// </remarks>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void QuerySavedSearchesShowDemo(TwitterContext twitterCtx)
        {
            var savedSearches =
                from search in twitterCtx.SavedSearch
                where search.Type == SavedSearchType.Show &&
                      search.ID == "176136"
                select search;

            var savedSearch = savedSearches.FirstOrDefault();

            Console.WriteLine("ID: {0}, Search: {1}", savedSearch.ID, savedSearch.Name);
        }

        /// <summary>
        /// shows how to retrieve all searches
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void QuerySavedSearchesDemo(TwitterContext twitterCtx)
        {
            var savedSearches =
                from search in twitterCtx.SavedSearch
                where search.Type == SavedSearchType.Searches
                select search;

            foreach (var search in savedSearches)
            {
                Console.WriteLine("ID: {0}, Search: {1}", search.ID, search.Name);
            }
        }

        #endregion

        #region OAuth Demos

        private static void InitializeOAuthConsumerStrings(TwitterContext twitterCtx)
        {
            var oauth = (DesktopOAuthAuthorization)twitterCtx.AuthorizedClient;
            oauth.GetVerifier = () =>
            {
                Console.WriteLine("Next, you'll need to tell Twitter to authorize access.\nThis program will not have access to your credentials, which is the benefit of OAuth.\nOnce you log into Twitter and give this program permission,\n come back to this console.");
                Console.Write("Please enter the PIN that Twitter gives you after authorizing this client: ");
                return Console.ReadLine();
            };



            if (oauth.CachedCredentialsAvailable)
            {
                Console.WriteLine("Skipping OAuth authorization step because that has already been done.");
            }
        }

        /// <summary>
        /// Shows how to force user to log in
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void OAuthForceLoginDemo(TwitterContext twitterCtx)
        {
            if (twitterCtx.AuthorizedClient.IsAuthorized)
            {
                var tweets =
                    from tweet in twitterCtx.Status
                    where tweet.Type == StatusType.Friends
                    select tweet;

                tweets.ToList().ForEach(
                    tweet => Console.WriteLine(
                        "Friend: {0}\nTweet: {1}\n",
                        tweet.User.Name,
                        tweet.Text));
            }
        }

        /// <summary>
        /// shows how to retrieve the screen name and user ID from an OAuth request
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void HandleOAuthRequestResponseDetailsDemo(TwitterContext twitterCtx)
        {
            Console.WriteLine();
            Console.WriteLine(
                "Screen Name: {0}, User ID: {1}",
                twitterCtx.AuthorizedClient.ScreenName,
                twitterCtx.AuthorizedClient.UserId);
        }

        /// <summary>
        /// shows what happens when performing a side-effect when ReadOnly is turned on
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void HandleOAuthSideEffectReadOnlyDemo(TwitterContext twitterCtx)
        {
            if (twitterCtx.AuthorizedClient.IsAuthorized)
            {
                var status = twitterCtx.UpdateStatus("I used LINQ to Twitter with OAuth: " + DateTime.Now.ToString());

                Console.WriteLine(
                    "Friend: {0}\nTweet: {1}\n",
                    status.User.Name,
                    status.Text);
            }
        }

        /// <summary>
        /// shows how to restrict access to read-only while performing a query
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void HandleOAuthReadOnlyQueryDemo(TwitterContext twitterCtx)
        {
            if (twitterCtx.AuthorizedClient.IsAuthorized)
            {
                var accounts =
                    from acct in twitterCtx.Account
                    where acct.Type == AccountType.VerifyCredentials
                    select acct;

                foreach (var account in accounts)
                {
                    Console.WriteLine("Credentials for account, {0}, are okay.", account.User.Name);
                }
            }
        }

        /// <summary>
        /// hows how to use OAuth to post a file to Twitter
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void HandleOAuthFilePostDemo(TwitterContext twitterCtx)
        {
            if (twitterCtx.AuthorizedClient.IsAuthorized)
            {
                var user = twitterCtx.UpdateAccountBackgroundImage(
                    @"C:\Users\jmayo\Documents\linq2twitter\linq2twitter\200xColor_2.png", false);

                Console.WriteLine(
                    "Name: {0}\nImage: {1}\n",
                    user.Name,
                    user.ProfileBackgroundImageUrl);
            }
        }

        /// <summary>
        /// Perform an update using OAuth
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void HandleOAuthSideEffectDemo(TwitterContext twitterCtx)
        {
            if (twitterCtx.AuthorizedClient.IsAuthorized)
            {
                var status = twitterCtx.UpdateStatus("I used LINQ to Twitter with OAuth: " + DateTime.Now.ToString());

                Console.WriteLine(
                    "Friend: {0}\nTweet: {1}\n",
                    status.User.Name,
                    status.Text);
            }
        }

        /// <summary>
        /// Shows how to update the background image with OAuth
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void HandleOAuthUpdateAccountBackgroundImageWithProgressUpdatesDemo(TwitterContext twitterCtx)
        {
            if (twitterCtx.AuthorizedClient.IsAuthorized)
            {
                twitterCtx.UploadProgressChanged +=
                        (sender, e) =>
                        {
                            Console.WriteLine("Progress: {0}%", e.PercentComplete);
                        };
                byte[] fileBytes = Utilities.GetFileBytes(@"C:\Users\jmayo\Documents\linq2twitter\linq2twitter\200xColor_2.png");
                var user = twitterCtx.UpdateAccountBackgroundImage(fileBytes, "200xColor_2.png", "png", false);

                Console.WriteLine("User Image: " + user.ProfileBackgroundImageUrl); 
            }
        }

        /// <summary>
        /// perform a query using OAuth
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void HandleOAuthQueryDemo(TwitterContext twitterCtx)
        {
            if (twitterCtx.AuthorizedClient.IsAuthorized)
            {
                var tweets =
                    from tweet in twitterCtx.Status
                    where tweet.Type == StatusType.Friends
                    select tweet;

                tweets.ToList().ForEach(
                    tweet => Console.WriteLine(
                        "Friend: {0}, Created: {1}\nTweet: {2}\n",
                        tweet.User.Name,
                        tweet.CreatedAt,
                        tweet.Text));
            }
        }

        #endregion

        #region Error Handling Demos

        /// <summary>
        /// shows how to handle a timeout error
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void HandleTimeoutErrors(TwitterContext twitterCtx)
        {
            // force an unreasonable timeout (1 millisecond)
            twitterCtx.Timeout = 1;

            var publicTweets =
                from tweet in twitterCtx.Status
                where tweet.Type == StatusType.Public
                select tweet;

            try
            {
                publicTweets.ToList().ForEach(
                        tweet => Console.WriteLine(
                            "User Name: {0}, Tweet: {1}",
                            tweet.User.Name,
                            tweet.Text));
            }
            catch (TwitterQueryException tqEx)
            {
                // use your logging and handling logic here

                // notice how the WebException is wrapped as the
                // inner exception of the TwitterQueryException
                Console.WriteLine(tqEx.InnerException.Message);
            }
        }

        /// <summary>
        /// shows how to handle a TwitterQueryException with a side-effect causing a file post
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void HandleSideEffectWithFilePostExceptionDemo(TwitterContext twitterCtx)
        {
            // force the error by supplying bad credentials
            twitterCtx.AuthorizedClient = new UsernamePasswordAuthorization
            {
                UserName = "BadUserName",
                Password = "BadPassword",
            };

            try
            {
                var user = twitterCtx.UpdateAccountImage(@"C:\Users\jmayo\Pictures\JoeTwitter.jpg");
            }
            catch (TwitterQueryException tqe)
            {
                // log it to the console
                Console.WriteLine(
                    "\nHTTP Error Code: {0}\nError: {1}\nRequest: {2}\n",
                    tqe.HttpError,
                    tqe.Response.Error,
                    tqe.Response.Request);
            }
        }

        /// <summary>
        /// shows how to handle a TwitterQueryException with a side-effect
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void HandleSideEffectExceptionDemo(TwitterContext twitterCtx)
        {
            // force the error by supplying bad credentials
            twitterCtx.AuthorizedClient = new UsernamePasswordAuthorization
            {
                UserName = "BadUserName",
                Password = "BadPassword",
            };

            try
            {
                var status = twitterCtx.UpdateStatus("Test from LINQ to Twitter - 5/2/09");
            }
            catch (TwitterQueryException tqe)
            {
                // log it to the console
                Console.WriteLine(
                    "\nHTTP Error Code: {0}\nError: {1}\nRequest: {2}\n",
                    tqe.HttpError,
                    tqe.Response.Error,
                    tqe.Response.Request);
            }
        }

        /// <summary>
        /// shows how to handle a TwitterQueryException with a query
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void HandleQueryExceptionDemo(TwitterContext twitterCtx)
        {
            // force the error by supplying bad credentials
            twitterCtx.AuthorizedClient = new UsernamePasswordAuthorization
            {
                UserName = "BadUserName",
                Password = "BadPassword",
            };

            try
            {
                var statuses =
                        from status in twitterCtx.Status
                        where status.Type == StatusType.Mentions
                        select status;

                var statusList = statuses.ToList();
            }
            catch (TwitterQueryException tqe)
            {
                // log it to the console
                Console.WriteLine(
                    "\nHTTP Error Code: {0}\nError: {1}\nRequest: {2}\n",
                    tqe.HttpError,
                    tqe.Response.Error,
                    tqe.Response.Request);
            }
        }

        #endregion

        #region Trends Demos

        /// <summary>
        /// shows how to request weekly trends
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void SearchWeeklyTrendsDemo(TwitterContext twitterCtx)
        {
            // remember to truncate seconds (maybe even minutes) because they
            // will never compare evenly, causing your list to be empty
            var trends =
                from trend in twitterCtx.Trends
                where trend.Type == TrendType.Weekly &&
                      trend.ExcludeHashtags == true &&
                      trend.Date == DateTime.Now.AddDays(-14).Date // <-- no time part
                select trend;

            trends.ToList().ForEach(
                trend => Console.WriteLine(
                    "Name: {0}, Query: {1}, Date: {2}",
                    trend.Name, trend.Query, trend.AsOf));
        }

        /// <summary>
        /// shows how to request daily trends
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void SearchDailyTrendsDemo(TwitterContext twitterCtx)
        {
            // remember to truncate seconds (maybe even minutes) because they
            // will never compare evenly, causing your list to be empty
            var trends =
                (from trend in twitterCtx.Trends
                 where trend.Type == TrendType.Daily &&
                       trend.Date == DateTime.Now.AddDays(-2).Date // <-- no time part
                 select trend)
                 .ToList();

            trends.ForEach(
                trend => Console.WriteLine(
                    "Name: {0}, Query: {1}",
                    trend.Name, trend.Query));
        }

        /// <summary>
        /// shows how to request current trends
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void SearchCurrentTrendsDemo(TwitterContext twitterCtx)
        {
            var trends =
                from trend in twitterCtx.Trends
                where trend.Type == TrendType.Current &&
                      trend.ExcludeHashtags == true
                select trend;

            trends.ToList().ForEach(
                trend => Console.WriteLine(
                    "Name: {0}, Query: {1}",
                    trend.Name, trend.Query));
        }

        /// <summary>
        /// shows how to request trends
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void SearchTrendsDemo(TwitterContext twitterCtx)
        {
            var trends =
                from trend in twitterCtx.Trends
                where trend.Type == TrendType.Trend
                select trend;

            trends.ToList().ForEach(
                trend => Console.WriteLine(
                    "Name: {0}, Query: {1}, Date: {2}",
                    trend.Name, trend.Query, trend.AsOf));
        }

        #endregion

        #region Account Demos

        /// <summary>
        /// Shows how to update account profile info
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void UpdateAccountInfoDemo(TwitterContext twitterCtx)
        {
            var user = twitterCtx.UpdateAccountProfile(
                "LINQ to Tweeter Test",
                "Joe@LinqToTwitter.com",
                "http://linqtotwitter.codeplex.com",
                "Anywhere In The World",
                "Testing the LINQ to Twitter Account Profile Update.");

            Console.WriteLine(
                "Name: {0}\nURL: {2}\nLocation: {3}\nDescription: {4}",
                user.Name, user.URL, user.Location, user.Description);
        }

        /// <summary>
        /// Shows how to update the background image in an account
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void UpdateAccountBackgroundImage(TwitterContext twitterCtx)
        {
            var user = twitterCtx.UpdateAccountBackgroundImage(@"C:\Users\jmayo\Documents\linq2twitter\linq2twitter\linq2twitter_v3_300x90.png", false);

            Console.WriteLine("User Image: " + user.ProfileBackgroundImageUrl);
        }

        /// <summary>
        /// Shows how to update the background image in an account
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void UpdateAccountBackgroundImageBytes(TwitterContext twitterCtx)
        {
            byte[] fileBytes = Utilities.GetFileBytes(@"C:\Users\jmayo\Documents\linq2twitter\linq2twitter\200xColor_2.png");
            var user = twitterCtx.UpdateAccountBackgroundImage(fileBytes, "200xColor_2.png", "png", false);

            Console.WriteLine("User Image: " + user.ProfileBackgroundImageUrl);
        }

        /// <summary>
        /// Shows how to update the background image in an account and tiles the image
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void UpdateAccountBackgroundImageAndTileDemo(TwitterContext twitterCtx)
        {
            byte[] fileBytes = Utilities.GetFileBytes(@"C:\Users\jmayo\Documents\linq2twitter\linq2twitter\linq2twitter_v3_300x90.png");
            var user = twitterCtx.UpdateAccountBackgroundImage(fileBytes, "linq2twitter_v3_300x90.png", "png", true);

            Console.WriteLine("User Image: " + user.ProfileBackgroundImageUrl);
        }

        /// <summary>
        /// Shows how to update the background image in an account
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void UpdateAccountBackgroundImageWithProgressUpdates(TwitterContext twitterCtx)
        {
            twitterCtx.UploadProgressChanged +=
                (sender, e) =>
                {
                    Console.WriteLine("Progress: {0}%", e.PercentComplete);
                };
            byte[] fileBytes = Utilities.GetFileBytes(@"C:\Users\jmayo\Documents\linq2twitter\linq2twitter\200xColor_2.png");
            var user = twitterCtx.UpdateAccountBackgroundImage(fileBytes, "200xColor_2.png", "png", false);

            Console.WriteLine("User Image: " + user.ProfileBackgroundImageUrl);
        }

        /// <summary>
        /// Shows how to update the image in an account
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void UpdateAccountImage(TwitterContext twitterCtx)
        {
            var user = twitterCtx.UpdateAccountImage(@"C:\Users\jmayo\Pictures\Sgt Peppers\JoeTwitterBW.jpg");

            Console.WriteLine("User Image: " + user.ProfileImageUrl);
        }

        /// <summary>
        /// Shows how to update Twitter colors
        /// </summary>
        /// <param name="twitterCtx"></param>
        private static void UpdateAccountColors(TwitterContext twitterCtx)
        {
            var user = twitterCtx.UpdateAccountColors("9ae4e8", "#000000", "#0000ff", "#e0ff92", "#87bc44");

            Console.WriteLine("\nAccount Colors:\n");

            Console.WriteLine("Background:     " + user.ProfileBackgroundColor);
            Console.WriteLine("Text:           " + user.ProfileTextColor);
            Console.WriteLine("Link:           " + user.ProfileLinkColor);
            Console.WriteLine("Sidebar Fill:   " + user.ProfileSidebarFillColor);
            Console.WriteLine("Sidebar Border: " + user.ProfileSidebarBorderColor);
        }

        /// <summary>
        /// Shows how to update a device
        /// </summary>
        /// <param name="twitterCtx"></param>
        private static void UpdateDeliveryDevice(TwitterContext twitterCtx)
        {
            var user = twitterCtx.UpdateAccountDeliveryDevice(DeviceType.None);

            Console.WriteLine("Device Type: {0}", user.Notifications.ToString());
        }

        /// <summary>
        /// Shows how to end the session for the current account
        /// </summary>
        /// <param name="twitterCtx"></param>
        private static void EndSession(TwitterContext twitterCtx)
        {
            var endSessionStatus = twitterCtx.EndAccountSession();

            Console.WriteLine(
                "Request: {0}, Error: {1}",
                endSessionStatus.Request,
                endSessionStatus.Error);
        }

        /// <summary>
        /// Shows how to extract rate limit info from response headers
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void ViewRateLimitResponseHeadersDemo(TwitterContext twitterCtx)
        {
            var myMentions =
                from mention in twitterCtx.Status
                where mention.Type == StatusType.Mentions
                select mention;

            Console.WriteLine("\nAll rate limit results are either -1 or from the last query because this query hasn't executed yet. Look at results for this query *after* the query: \n");

            Console.WriteLine("Current Rate Limit: {0}", twitterCtx.RateLimitCurrent);
            Console.WriteLine("Remaining Rate Limit: {0}", twitterCtx.RateLimitRemaining);
            Console.WriteLine("Rate Limit Reset: {0}", twitterCtx.RateLimitReset);

            myMentions.ToList().ForEach(
                mention => Console.WriteLine(
                    "Name: {0}, Tweet: {1}\n",
                    mention.User.Name, mention.Text));

            Console.WriteLine("\nRate Limits from Query Response: \n");

            Console.WriteLine("Current Rate Limit: {0}", twitterCtx.RateLimitCurrent);
            Console.WriteLine("Remaining Rate Limit: {0}", twitterCtx.RateLimitRemaining);
            Console.WriteLine("Rate Limit Reset: {0}", twitterCtx.RateLimitReset);

            var resetTime =
                new DateTime(1970, 1, 1)
                .AddSeconds(twitterCtx.RateLimitReset)
                .ToLocalTime();

            Console.WriteLine("Rate Limit Reset in current time: {0}", resetTime);
        }

        /// <summary>
        /// Shows how to query an account's rate limit status info
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void ViewRateLimitStatus(TwitterContext twitterCtx)
        {
            var accounts =
                from acct in twitterCtx.Account
                where acct.Type == AccountType.RateLimitStatus
                select acct;

            foreach (var account in accounts)
            {
                Console.WriteLine("\nRate Limit Status: \n");
                Console.WriteLine("Remaining Hits: {0}", account.RateLimitStatus.RemainingHits);
                Console.WriteLine("Hourly Limit: {0}", account.RateLimitStatus.HourlyLimit);
                Console.WriteLine("Reset Time: {0}", account.RateLimitStatus.ResetTime);
                Console.WriteLine("Reset Time in Seconds: {0}", account.RateLimitStatus.ResetTimeInSeconds);
            }
        }

        /// <summary>
        /// verifies that account credentials are correct
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void VerifyAccountCredentials(TwitterContext twitterCtx)
        {
            var accounts =
                from acct in twitterCtx.Account
                where acct.Type == AccountType.VerifyCredentials
                select acct;

            try
            {
                foreach (var account in accounts)
                {
                    Console.WriteLine("Credentials for account, {0}, are okay.", account.User.Name);
                }
            }
            catch (WebException wex)
            {
                Console.WriteLine("Twitter did not recognize the credentials. Response from Twitter: " + wex.Message);
            }
        }

        #endregion

        #region Help Demos

        /// <summary>
        /// shows how to perform a help test
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void PerformHelpTest(TwitterContext twitterCtx)
        {
            var helpResult = twitterCtx.HelpTest();

            Console.WriteLine("Test Result: " + helpResult);
        }

        #endregion

        #region Block Demos

        /// <summary>
        /// shows how to unblock a user
        /// </summary>
        /// <param name="twitterCtx"></param>
        private static void DestroyBlock(TwitterContext twitterCtx)
        {
            var user = twitterCtx.DestroyBlock("JoeMayo");

            if (user == null) return;

            Console.WriteLine("User Name: " + user.Name);
        }

        /// <summary>
        /// Shows how to block a user
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void CreateBlock(TwitterContext twitterCtx)
        {
            var user = twitterCtx.CreateBlock("JoeMayo");

            if (user == null) return;

            Console.WriteLine("User Name: " + user.Name);
        }

        /// <summary>
        /// shows how to get a list of users that are being blocked
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void BlockBlockingDemo(TwitterContext twitterCtx)
        {
            var block =
                (from blockItem in twitterCtx.Blocks
                 where blockItem.Type == BlockingType.Blocking
                 select blockItem)
                 .FirstOrDefault();

            block.Users.ForEach(
                user => Console.WriteLine("User, {0} is blocked.", user.Name));
        }

        /// <summary>
        /// shows how to get a list of IDs of the users being blocked
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void BlockIDsDemo(TwitterContext twitterCtx)
        {
            var result =
                (from blockItem in twitterCtx.Blocks
                 where blockItem.Type == BlockingType.IDS
                 select blockItem)
                 .SingleOrDefault();

            result.IDs.ForEach(block => Console.WriteLine("ID: {0}", block));
        }

        /// <summary>
        /// shows how to see if a specific user is being blocked
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void BlockExistsDemo(TwitterContext twitterCtx)
        {
            try
            {
                var result =
                    from blockItem in twitterCtx.Blocks
                    where blockItem.Type == BlockingType.Exists &&
                          blockItem.ScreenName == "JoeMayo"
                    select blockItem;

                result.ToList().ForEach(
                    block =>
                        Console.WriteLine(
                            "User, {0} is blocked.",
                            block.User.Name));
            }
            catch (TwitterQueryException tqe)
            {
                // Twitter returns HTTP 404 when user is not blocked
                // An HTTP error generates an exception, 
                // which is why User Not Blocked is handled this way
                Console.WriteLine("User not blocked. Twitter Response: " + tqe.Response.Error);
            }
        }

        #endregion

        #region Notifications Demos

        /// <summary>
        /// Shows how to do a Notifications Follow
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void EnableNotificationsDemo(TwitterContext twitterCtx)
        {
            var user = twitterCtx.EnableNotifications("15411837", null, null);

            if (user == null) return;

            Console.WriteLine("User Name: " + user.Name);
        }

        /// <summary>
        /// Shows how to do a Notifications Follow
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void EnableNotificationsWithScreenNameDemo(TwitterContext twitterCtx)
        {
            var user = twitterCtx.EnableNotifications(null, null, "JoeMayo");

            if (user == null) return;

            Console.WriteLine("User Name: " + user.Name);
        }

        /// <summary>
        /// Shows how to do a Notifications Follow
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void EnableNotificationsWithUserIDDemo(TwitterContext twitterCtx)
        {
            var user = twitterCtx.EnableNotifications(null, "15411837", null);

            if (user == null) return;

            Console.WriteLine("User Name: " + user.Name);
        }

        /// <summary>
        /// Shows how to do a Notifications Leave
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void DisableNotificationsDemo(TwitterContext twitterCtx)
        {
            var user = twitterCtx.DisableNotifications("15411837", null, null);

            if (user == null) return;

            Console.WriteLine("User Name: " + user.Name);
        }

        #endregion

        #region Favorites Demos

        private static void DestroyFavoriteDemo(TwitterContext twitterCtx)
        {
            var status = twitterCtx.DestroyFavorite("1552797863");

            Console.WriteLine("User: {0}, Tweet: {1}", status.User.Name, status.Text);
        }

        /// <summary>
        /// Shows how to create a Favorite
        /// </summary>
        /// <param name="twitterCtx"></param>
        private static void CreateFavoriteDemo(TwitterContext twitterCtx)
        {
            var status = twitterCtx.CreateFavorite("1552797863");

            Console.WriteLine("User: {0}, Tweet: {1}", status.User.Name, status.Text);
        }

        /// <summary>
        /// shows how to request a favorites list
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void FavoritesQueryDemo(TwitterContext twitterCtx)
        {
            var favorites =
                from fav in twitterCtx.Favorites
                where fav.Type == FavoritesType.Favorites
                select fav;

            favorites.ToList().ForEach(
                fav => Console.WriteLine(
                    "User Name: {0}, Tweet: {1}",
                    fav.User.Name, fav.Text));
        }

        #endregion

        #region Search Demos

        /// <summary>
        /// shows how to perform a twitter search
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void SearchTwitterDemo(TwitterContext twitterCtx)
        {
            
            var queryResults =
                from search in twitterCtx.Search
                where search.Type == SearchType.Search &&
                      search.Query == "LINQ to Twitter" &&
                      search.Page == 2 &&
                      search.PageSize == 5
                select search;

            foreach (var search in queryResults)
            {
                // here, you can see that properties are named
                // from the perspective of atom feed elements
                // i.e. the query string is called Title
                Console.WriteLine("\nQuery:\n" + search.Title);

                foreach (var entry in search.Entries)
                {
                    Console.WriteLine(
                        "ID: {0}, Source: {1}\nContent: {2}\n",
                        entry.ID, entry.Source, entry.Content);
                }
            }
        }

        /// <summary>
        /// Shows how to specify a source of tweets to search for
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void SearchTwitterSource(TwitterContext twitterCtx)
        {
            var queryResults =
                from search in twitterCtx.Search
                where search.Type == SearchType.Search &&
                      search.Query == "LINQ to Twitter source:web"
                select search;

            foreach (var search in queryResults)
            {
                // here, you can see that properties are named
                // from the perspective of atom feed elements
                // i.e. the query string is called Title
                Console.WriteLine("\nQuery:\n" + search.Title);

                foreach (var entry in search.Entries)
                {
                    Console.WriteLine(
                        "ID: {0}, Source: {1}\nContent: {2}\n",
                        entry.ID, entry.Source, entry.Content);
                }
            }
        }

        ///// <summary>
        ///// shows how to handle response when you exceed Search query rate limits
        ///// </summary>
        ///// <param name="twitterCtx"></param>
        //private static void ExceedSearchRateLimitDemo(TwitterContext twitterCtx)
        //{
        //    //
        //    // WARNING: This is for Test/Demo purposes only; 
        //    //          it makes many queries to Twitter in
        //    //          a very short amount of time, which
        //    //          has a negative impact on the service.
        //    //
        //    //          The only reason it is here is to test
        //    //          that LINQ to Twitter responds appropriately
        //    //          to Search rate limits.
        //    //

        //    var queryResults =
        //        from search in twitterCtx.Search
        //        where search.Type == SearchType.Search &&
        //              search.Query == "Testing Search Rate Limit Results"
        //        select search;

        //    try
        //    {
        //        // set to a sufficiently high number to force the HTTP 503 response
        //        // -- assumes you have the bandwidth to exceed
        //        //    limit, which you might not have
        //        var searchesToPerform = 5;

        //        for (int i = 0; i < searchesToPerform; i++)
        //        {
        //            foreach (var search in queryResults)
        //            {
        //                // here, you can see that properties are named
        //                // from the perspective of atom feed elements
        //                // i.e. the query string is called Title
        //                Console.WriteLine("\n#{0}. Query:{1}\n", i+1, search.Title);

        //                foreach (var entry in search.Entries)
        //                {
        //                    Console.WriteLine(
        //                        "ID: {0}, Source: {1}\nContent: {2}\n",
        //                        entry.ID, entry.Source, entry.Content);
        //                }
        //            } 
        //        }
        //    }
        //    catch (TwitterQueryException tqe)
        //    {
        //        if (tqe.HttpError == "503")
        //        {
        //            Console.WriteLine("HTTP Error: {0}", tqe.HttpError);
        //            Console.WriteLine("You've exceeded rate limits for search.");
        //            Console.WriteLine("Please retry in {0} seconds.", twitterCtx.RetryAfter);
        //        }
        //    }

        //    Console.WriteLine("\nComplete.");
        //}

        #endregion

        #region Social Graph Demos

        /// <summary>
        /// Shows how to list followers
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void ShowFollowersDemo(TwitterContext twitterCtx)
        {
            var followers =
                (from follower in twitterCtx.SocialGraph
                 where follower.Type == SocialGraphType.Followers &&
                       follower.ID == "15411837"
                 select follower)
                 .SingleOrDefault();

            followers.IDs.ForEach(id => Console.WriteLine("Follower ID: " + id));
        }

        /// <summary>
        /// Pages through a list of followers using cursors
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void ShowFollowersWithCursorDemo(TwitterContext twitterCtx)
        {
            int pageNumber = 1;

            // "-1" means to begin on the first page
            string nextCursor = "-1";

            // cursor will be "0" when no more pages
            // notice that I'm checking for null/empty - don't trust data
            while (!string.IsNullOrEmpty(nextCursor) && nextCursor != "0")
            {
                var followers =
                    (from follower in twitterCtx.SocialGraph
                     where follower.Type == SocialGraphType.Followers &&
                           follower.ID == "15411837" &&
                           follower.Cursor == nextCursor // <-- set this to use cursors
                     select follower)
                     .FirstOrDefault();

                Console.WriteLine(
                    "Page #" + pageNumber + " has " + followers.IDs.Count + " IDs.");

                // use the cursor for the next page
                // this is not a page number, but a marker (cursor)
                // to tell Twitter which page to return
                nextCursor = followers.CursorMovement.Next;
                pageNumber++;
            }
        }

        /// <summary>
        /// Shows how to list Friends
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void ShowFriendsDemo(TwitterContext twitterCtx)
        {
            var friendList =
                (from friend in twitterCtx.SocialGraph
                 where friend.Type == SocialGraphType.Friends &&
                       friend.ScreenName == "JoeMayo"
                 select friend)
                 .SingleOrDefault();

            foreach (var id in friendList.IDs)
            {
                Console.WriteLine("Friend ID: " + id);
            }
        }

        /// <summary>
        /// Pages through a list of followers using cursors
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void ShowFriendsWithCursorDemo(TwitterContext twitterCtx)
        {
            int pageNumber = 1;
            string nextCursor = "-1";
            while (!string.IsNullOrEmpty(nextCursor) && nextCursor != "0")
            {
                var friends =
                    (from friend in twitterCtx.SocialGraph
                     where friend.Type == SocialGraphType.Friends &&
                           friend.ScreenName == "JoeMayo" &&
                           friend.Cursor == nextCursor
                     select friend)
                     .SingleOrDefault();

                Console.WriteLine(
                    "Page #" + pageNumber + " has " + friends.IDs.Count + " IDs.");

                nextCursor = friends.CursorMovement.Next;
                pageNumber++;
            }
        }


        #endregion

        #region Friendship Demos

        private static void CreateFriendshipNoDeviceUpdatesDemo(TwitterContext twitterCtx)
        {
            var user = twitterCtx.CreateFriendship("LinqToTweeter", string.Empty, string.Empty, false);

            Console.WriteLine(
                "User Name: {0}, Status: {1}",
                user.Name,
                user.Status.Text);
        }

        private static void DestroyFriendshipDemo(TwitterContext twitterCtx)
        {
            var user = twitterCtx.DestroyFriendship("LinqToTweeter", string.Empty, string.Empty);

            Console.WriteLine(
                "User Name: {0}, Status: {1}",
                user.Name,
                user.Status.Text);
        }

        private static void CreateFriendshipFollowDemo(TwitterContext twitterCtx)
        {
            var user = twitterCtx.CreateFriendship("LinqToTweeter", string.Empty, string.Empty, true);

            Console.WriteLine(
                "User Name: {0}, Status: {1}",
                user.Name,
                user.Status.Text);
        }

        /// <summary>
        /// shows how to show that one user follows another with Friendship Exists
        /// </summary>
        /// <param name="twitterCtx"></param>
        private static void FriendshipExistsDemo(TwitterContext twitterCtx)
        {
            var friendship =
                (from friend in twitterCtx.Friendship
                 where friend.Type == FriendshipType.Exists &&
                       friend.SubjectUser == "JoeMayo" &&
                       friend.FollowingUser == "LinqToTweeter"
                 select friend)
                 .ToList();

            Console.WriteLine(
                "LinqToTweeter follows JoeMayo: " +
                friendship.First().IsFriend);
        }

        #endregion

        #region Direct Message Demos

        /// <summary>
        /// shows how to delete a direct message
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void DestroyDirectMessageDemo(TwitterContext twitterCtx)
        {
            var message = twitterCtx.DestroyDirectMessage("96404341");

            if (message != null)
            {
                Console.WriteLine(
                    "Recipient: {0}, Message: {1}",
                    message.RecipientScreenName,
                    message.Text);
            }
        }

        /// <summary>
        /// shows how to send a new direct message
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void NewDirectMessageDemo(TwitterContext twitterCtx)
        {
            var message = twitterCtx.NewDirectMessage("16761255", "Direct Message Test - " + DateTime.Now);

            if (message != null)
            {
                Console.WriteLine(
                    "Recipient: {0}, Message: {1}, Date: {2}",
                    message.RecipientScreenName,
                    message.Text,
                    message.CreatedAt);
            }
        }

        /// <summary>
        /// shows how to query direct messages
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void DirectMessageSentToQueryDemo(TwitterContext twitterCtx)
        {
            var directMessages =
                (from tweet in twitterCtx.DirectMessage
                 where tweet.Type == DirectMessageType.SentTo &&
                       tweet.Count == 2
                 select tweet)
                 .ToList();

            directMessages.ForEach(
                dm => Console.WriteLine(
                    "Sender: {0}, Tweet: {1}",
                    dm.SenderScreenName,
                    dm.Text));
        }

        /// <summary>
        /// shows how to query direct messages
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void DirectMessageSentByQueryDemo(TwitterContext twitterCtx)
        {
            var directMessages =
                (from tweet in twitterCtx.DirectMessage
                 where tweet.Type == DirectMessageType.SentBy &&
                       tweet.Count == 2
                 select tweet)
                 .ToList();

            directMessages.ForEach(
                dm => Console.WriteLine(
                    "Sender: {0}, Tweet: {1}",
                    dm.SenderScreenName,
                    dm.Text));
        }

        #endregion

        #region User Demos

        /// <summary>
        /// shows how to query users
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void UserShowWithScreenNameQueryDemo(TwitterContext twitterCtx)
        {
            var users =
                from tweet in twitterCtx.User
                where tweet.Type == UserType.Show &&
                      tweet.ScreenName == "JoeMayo"
                select tweet;

            var user = users.SingleOrDefault();

            var name = user.Name;
            var lastStatus = user.Status == null ? "No Status" : user.Status.Text;

            Console.WriteLine();
            Console.WriteLine("Name: {0}, Last Tweet: {1}\n", name, lastStatus);
        }
        
        /// <summary>
        /// shows how to query users
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void UserShowWithIDQueryDemo(TwitterContext twitterCtx)
        {
            var users =
                from tweet in twitterCtx.User
                where tweet.Type == UserType.Show &&
                      tweet.ID == "15411837"
                select tweet;

            var user = users.SingleOrDefault();

            Console.WriteLine(
                "Name: {0}, Last Tweet: {1}\n",
                user.Name, user.Status.Text);
        }

        /// <summary>
        /// shows how to query friends of a specified user
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void UserFriendsQueryDemo(TwitterContext twitterCtx)
        {
            var users =
                from tweet in twitterCtx.User
                where tweet.Type == UserType.Friends &&
                      tweet.ID == "15411837" // <-- user to get friends for
                select tweet;

            foreach (var user in users)
            {
                var status = 
                    user.Protected || user.Status == null ? 
                        "Status Unavailable" : 
                        user.Status.Text;

                Console.WriteLine(
                        "ID: {0}, Name: {1}\nLast Tweet: {2}\n",
                        user.ID, user.Name, status);
            }
        }

        /// <summary>
        /// shows how to check the verified and geoenabled tags for users
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void VerifiedAndGeoEnabledDemo(TwitterContext twitterCtx)
        {
            var users =
                from tweet in twitterCtx.User
                where tweet.Type == UserType.Friends &&
                      tweet.ID == "15411837" // <-- user to get friends for
                select tweet;

            foreach (var user in users)
            {
                var status =
                    user.Protected || user.Status == null ?
                        "Status Unavailable" :
                        user.Status.Text;

                Console.WriteLine(
                        "ID: {0}, Verified: {1}, GeoEnabled: {2}, Name: {3}\nLast Tweet: {4}\n",
                        user.ID, user.Verified, user.GeoEnabled, user.Name, status);
            }
        }

        /// <summary>
        /// shows how to query friends of a specified user
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void UserFriendsWithCursorQueryDemo(TwitterContext twitterCtx)
        {
            int pageNumber = 1;
            string nextCursor = "-1";
            while (!string.IsNullOrEmpty(nextCursor) && nextCursor != "0")
            {
                var users =
                     (from user in twitterCtx.User
                      where user.Type == UserType.Friends &&
                            user.ID == "15411837" &&
                            user.Cursor == nextCursor
                      select user)
                      .ToList();

                Console.WriteLine(
                    "Page #" + pageNumber + " has " + users.Count + " users.");

                nextCursor = users[0].CursorMovement.Next;
                pageNumber++;
            }
        }

        /// <summary>
        /// shows how to query users
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void UserFollowersQueryDemo(TwitterContext twitterCtx)
        {
            int pageNumber = 1;
            string nextCursor = "-1";
            while (!string.IsNullOrEmpty(nextCursor) && nextCursor != "0")
            {
                var users =
                    (from user in twitterCtx.User
                     where user.Type == UserType.Followers &&
                           user.ID == "15411837" &&
                           user.Cursor == nextCursor
                     select user)
                     .ToList();

                Console.WriteLine(
                    "Page #" + pageNumber + " has " + users.Count + " users.");

                nextCursor = users[0].CursorMovement.Next;
                pageNumber++;
            }
        }

        /// <summary>
        /// shows how to query users
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void UserFollowersWithCursorsQueryDemo(TwitterContext twitterCtx)
        {
            var users =
                from tweet in twitterCtx.User
                where tweet.Type == UserType.Followers &&
                      tweet.ID == "15411837"
                select tweet;

            foreach (var user in users)
            {
                var status =
                    user.Protected || user.Status == null ?
                        "Status Unavailable" :
                        user.Status.Text;

                Console.WriteLine(
                        "Name: {0}, Last Tweet: {1}\n",
                        user.Name, status);
            }
        }

        /// <summary>
        /// shows how to query all followers
        /// </summary>
        /// <remarks>
        /// uses the Page property because Twitter doesn't
        /// return all followers in a single call; you
        /// must page through results until you get all
        /// </remarks>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void GetAllFollowersQueryDemo(TwitterContext twitterCtx)
        {
            //
            // Paging has been deprecated for Friends and Followers
            // Please use cursors instead
            //

            var followerList = new List<User>();

            List<User> followers = new List<User>();
            int pageNumber = 1;

            do
            {
                followers.Clear();

                followers =
                    (from follower in twitterCtx.User
                     where follower.Type == UserType.Followers &&
                           follower.ScreenName == "JoeMayo" &&
                           follower.Page == pageNumber
                     select follower)
                     .ToList();

                pageNumber++;
                followerList.AddRange(followers);
            }
            while (followers.Count > 0);

            Console.WriteLine("\nFollowers: \n");

            foreach (var user in followerList)
            {
                var status =
                    user.Protected || user.Status == null ?
                        "Status Unavailable" :
                        user.Status.Text;

                Console.WriteLine(
                        "Name: {0}, Last Tweet: {1}\n",
                        user.Name, status);
            }

            Console.WriteLine("\nFollower Count: {0}\n", followerList.Count);
        }

        #endregion

        #region Status Demos

        /// <summary>
        /// Shows how to get statuses for logged-in user's friends - just like main Twitter page
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void SingleStatusQueryDemo(TwitterContext twitterCtx)
        {
            var friendTweets =
                from tweet in twitterCtx.Status
                where tweet.Type == StatusType.Show &&
                      tweet.ID == "5087050961"
                select tweet;

            Console.WriteLine("\nRequested Tweet: \n");
            foreach (var tweet in friendTweets)
            {
                Console.WriteLine(
                    "User: " + tweet.User.Name +
                    "\nTweet: " + tweet.Text + 
                    "\nTweet ID: " + tweet.ID + "\n");
            }
        }

        /// <summary>
        /// Shows how to get statuses for logged-in user's friends - just like main Twitter page
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void FriendStatusQueryDemo(TwitterContext twitterCtx)
        {
            var friendTweets =
                from tweet in twitterCtx.Status
                where tweet.Type == StatusType.Friends
                select tweet;

            Console.WriteLine("\nTweets for " + twitterCtx.UserName + "\n");
            foreach (var tweet in friendTweets)
            {
                Console.WriteLine(
                    "Friend: " + tweet.User.Name +
                    "\nTweet: " + tweet.Text + "\n");
            }
        }

        /// <summary>
        /// Shows how to get statuses for logged-in user's friends, including retweets
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void HomeStatusQueryDemo(TwitterContext twitterCtx)
        {
            var friendTweets =
                from tweet in twitterCtx.Status
                where tweet.Type == StatusType.Home &&
                      tweet.Page == 2
                select tweet;

            Console.WriteLine("\nTweets for " + twitterCtx.UserName + "\n");
            foreach (var tweet in friendTweets)
            {
                Console.WriteLine(
                    "Friend: " + tweet.User.Name +
                    "\nRetweeted by: " + 
                        (tweet.Retweet == null ? 
                            "Original Tweet" :
                            tweet.Retweet.RetweetingUser.Name) +
                    "\nTweet: " + tweet.Text + "\n");
            }
        }

        /// <summary>
        /// Shows how to query tweets menioning logged-in user
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void MentionsStatusQueryDemo(TwitterContext twitterCtx)
        {
            var myMentions =
                from mention in twitterCtx.Status
                where mention.Type == StatusType.Mentions
                select mention;

            myMentions.ToList().ForEach(
                mention => Console.WriteLine(
                    "Name: {0}, Tweet[{1}]: {2}\n",
                    mention.User.Name, mention.StatusID, mention.Text));
        }

        private static void RetweetDemo(TwitterContext twitterCtx)
        {
            var retweet = twitterCtx.Retweet("5769361742");

            Console.WriteLine("Retweeted Tweet: ");
            Console.WriteLine(
                "\nUser: " + retweet.Retweet.RetweetingUser.Name +
                "\nTweet: " + retweet.Retweet.Text +
                "\nTweet ID: " + retweet.Retweet.ID + "\n");
        }

        /// <summary>
        /// Shows how to get retweets of a specified tweet
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void RetweetsQueryDemo(TwitterContext twitterCtx)
        {
            var friendTweets =
                from tweet in twitterCtx.Status
                where tweet.Type == StatusType.Retweets &&
                      tweet.ID == "6773457956"
                select tweet;

            Console.WriteLine("\nReTweets: \n");
            foreach (var tweet in friendTweets)
            {
                Console.WriteLine(
                    "\nUser: " + tweet.Retweet.RetweetingUser.Name +
                    "\nTweet: " + tweet.Retweet.Text +
                    "\nTweet ID: " + tweet.Retweet.ID + "\n");
            }
        }

        /// <summary>
        /// Shows how to query retweets by the logged-in user
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void RetweetedByMeStatusQueryDemo(TwitterContext twitterCtx)
        {
            var myRetweets =
                from retweet in twitterCtx.Status
                where retweet.Type == StatusType.RetweetedByMe
                select retweet;

            myRetweets.ToList().ForEach(
                retweet => Console.WriteLine(
                    "Name: {0}, Tweet: {1}\n",
                    retweet.Retweet.RetweetingUser.Name, retweet.Retweet.Text));
        }

        /// <summary>
        /// Shows how to query retweets to the logged-in user
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void RetweetedToMeStatusQueryDemo(TwitterContext twitterCtx)
        {
            var myRetweets =
                from retweet in twitterCtx.Status
                where retweet.Type == StatusType.RetweetedToMe
                select retweet;

            myRetweets.ToList().ForEach(
                retweet => Console.WriteLine(
                    "Name: {0}, Tweet: {1}\n",
                    retweet.Retweet.RetweetingUser.Name, retweet.Retweet.Text));
        }

        /// <summary>
        /// Shows how to query retweets about the logged-in user
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void RetweetsOfMeStatusQueryDemo(TwitterContext twitterCtx)
        {
            var myRetweets =
                from retweet in twitterCtx.Status
                where retweet.Type == StatusType.RetweetsOfMe
                select retweet;

            myRetweets.ToList().ForEach(
                retweet => Console.WriteLine(
                    "Name: {0}, Tweet: {1}\n",
                    retweet.User.Name, retweet.Text));
        }

        /// <summary>
        /// Shows how to get tweets and retweets by the logged-in user through a union
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void GetAllTweetsAndRetweetsDemo(TwitterContext twitterCtx)
        {
            var myTweets =
                (from tweet in twitterCtx.Status
                 where tweet.Type == StatusType.User
                      && tweet.ScreenName == "JoeMayo"
                 select tweet)
                 .ToList();

            var myRetweets =
                (from retweet in twitterCtx.Status
                 where retweet.Type == StatusType.RetweetedByMe
                 select retweet)
                 .ToList();

            var allTweets = myTweets.Union(myRetweets);

            allTweets.ToList().ForEach(
                tweet => 
                {
                    if (tweet.Retweet == null)
                    {
                        Console.WriteLine(
                            "Name: {0}, Tweet: {1}\n",
                            tweet.User.Name, tweet.Text);
                    }
                    else
	                {
                        Console.WriteLine(
                            "Name: {0}, ReTweet: {1}\n",
                            tweet.Retweet.RetweetingUser.Name, tweet.Retweet.Text);
	                }
                });
        }

        /// <summary>
        /// shows how to query status with a screen name for specified number of tweets
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void UserStatusByNameQueryDemo(TwitterContext twitterCtx)
        {
            Console.WriteLine();

            var lastN = 11;
            var screenName = "JoeMayo";

            var statusTweets =
                from tweet in twitterCtx.Status
                where tweet.Type == StatusType.User
                      && tweet.ScreenName == screenName
                      && tweet.Count == lastN
                select tweet;

            foreach (var tweet in statusTweets)
            {
                Console.WriteLine(
                    "(" + tweet.StatusID + ")" +
                    "[" + tweet.User.ID + "]" +
                    tweet.User.Name + ", " +
                    tweet.Text + ", " +
                    tweet.CreatedAt);
            }
        }

        /// <summary>
        /// shows how to query status
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void UserStatusQueryDemo(TwitterContext twitterCtx)
        {
            Console.WriteLine();

            var statusTweets =
                from tweet in twitterCtx.Status
                where tweet.Type == StatusType.User
                      && tweet.ID == "15411837"  // ID for User
                select tweet;

            foreach (var tweet in statusTweets)
            {
                Console.WriteLine(
                    "(" + tweet.StatusID + ")" +
                    "[" + tweet.User.ID + "]" +
                    tweet.User.Name + ", " +
                    tweet.Text + ", " +
                    tweet.CreatedAt);
            }
        }

        /// <summary>
        /// shows how to query status
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void FirstStatusQueryDemo(TwitterContext twitterCtx)
        {
            Console.WriteLine();

            var statusTweets =
                from tweet in twitterCtx.Status
                where tweet.Type == StatusType.User
                      && tweet.ID == "15411837"  // ID for User
                      && tweet.Page == 1
                      && tweet.Count == 20
                      && tweet.SinceID == 931894254
                select tweet;

            var status = statusTweets.FirstOrDefault();

            Console.WriteLine(
                "(" + status.StatusID + ")" +
                "[" + status.User.ID + "]" +
                status.User.Name + ", " +
                status.Text + ", " +
                status.CreatedAt);
        }

        /// <summary>
        /// shows how to delete a status
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void DestroyStatusDemo(TwitterContext twitterCtx)
        {
            var status = twitterCtx.DestroyStatus("1539399086");

            Console.WriteLine(
                "(" + status.StatusID + ")" +
                "[" + status.User.ID + "]" +
                status.User.Name + ", " +
                status.Text + ", " +
                status.CreatedAt);
        }

        /// <summary>
        /// shows how to update a status
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void UpdateStatusWithReplyDemo(TwitterContext twitterCtx)
        {
            var tweet = twitterCtx.UpdateStatus("@LinqToTweeter Testing LINQ to Twitter with reply on " + DateTime.Now.ToString() + " #linqtotwitter", "961760788");

            Console.WriteLine(
                "(" + tweet.StatusID + ")" +
                "[" + tweet.User.ID + "]" +
                tweet.User.Name + ", " +
                tweet.Text + ", " +
                tweet.CreatedAt);
        }

        /// <summary>
        /// shows how to update a status
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void UpdateStatusDemo(TwitterContext twitterCtx)
        {
            // the \u00C7 is C Cedilla, which I've included to ensure that non-ascii characters appear properly
            var status = "\u00C7 Testing LINQ to Twitter update status on " + DateTime.Now.ToString() + " #linqtotwitter";

            Console.WriteLine("Status being sent: " + status);

            var tweet = twitterCtx.UpdateStatus(status);

            Console.WriteLine(
                "Status returned: " +
                "(" + tweet.StatusID + ")" +
                "[" + tweet.User.ID + "]" +
                tweet.User.Name + ", " +
                tweet.Text + ", " +
                tweet.CreatedAt);
        }

        public class MyTweetClass
        {
            public string UserName { get; set; }
            public string Text { get; set; }
        }

        /// <summary>
        /// shows how to send a public status query and then filter
        /// </summary>
        /// <remarks>
        /// since Twitter API doesn't filter public status,
        /// you can grab the results and then filter with
        /// LINQ to Objects.
        /// </remarks>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void PublicStatusFilteredQueryDemo(TwitterContext twitterCtx)
        {
            //var publicTweets =
            //    (from tweet in twitterCtx.Status
            //     where tweet.Type == StatusType.Public &&
            //           tweet.User.Name.StartsWith("S")
            //     orderby tweet.Source descending
            //     select new MyTweetClass
            //     {
            //         UserName = tweet.User.Name,
            //         Text = tweet.Text
            //     })
            //     .ToArray();

            var publicTweets =
                (from tweet in twitterCtx.Status
                 where tweet.Type == StatusType.Public
                 orderby tweet.Source descending, tweet.User.Name
                 select tweet)
                 .ToArray();

            publicTweets.ToList().ForEach(
                tweet => Console.WriteLine(
                    "Source: {0}, Name: {1}",
                    tweet.Source,
                    tweet.User.Name));

            //publicTweets.ToList().ForEach(
            //    tweet => Console.WriteLine(
            //        "User Name: {0}, Tweet: {1}",
            //        tweet.User.Name,
            //        tweet.Text));

            //var publicTweets = twitterCtx.Status
            //    .Where(x => x.Type == StatusType.Public)
            //    .Select(x => x.Text.Replace('\n', ' '))
            //    .ToArray();

            //publicTweets.ToList().ForEach(
            //    tweet => Console.WriteLine(
            //        "Tweet: {0}",
            //        tweet));
        }

        /// <summary>
        /// shows how to send a public status query
        /// </summary>
        /// <param name="twitterCtx">TwitterContext</param>
        private static void PublicStatusQueryDemo(TwitterContext twitterCtx)
        {
            var publicTweets =
                from tweet in twitterCtx.Status
                where tweet.Type == StatusType.Public
                select tweet;

            publicTweets.ToList().ForEach(
                tweet => Console.WriteLine(
                    "User Name: {0}, Tweet: {1}",
                    tweet.User.Name,
                    tweet.Text));
        }

        #endregion
    }
}
