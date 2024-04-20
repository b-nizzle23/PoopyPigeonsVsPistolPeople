# How to use git!

## how to start using the program (only do this the first time):
```
$ git pull
$ git checkout -b YOURNAME
$ git branch                 |// this shows the branches that are in the repo, as well as marking which branch you are on with an '*' asteriks.

```


## what to do EVERY TIME you work on the project:
Repeat this process each time that you access the project
```
$ git checkout main         |// if this isn't done already
$ git pull
$ git checkout YOURNAME
$ git merge main
...                         |// proceed to write code. when you are done...
$ git add .
$ git commit -m "DESCRIPTION OF WHAT YOU CHANGED"
$ git push
$ git status                |// check that working tree is up to date
$ git checkout main
$ git merge YOURNAME        |// this will add your changes to the main program branch
$ git push origin main
```
**DO NOT EVER PUSH NON-WORKING CODE TO THE MAIN BRANCH**


### getting code from the repository onto your computer

`$ git pull` - this will take the current version of the repository and copy it onto your computer. If you have made changes on your side that do not match the changes made on the repo, there will be merge conflicts.  idk exactly how to resolve this unless I see it, so just make sure to start off every coding session with a pull request and finish each one with a push request. 

---
---

## Advanced git

### git branches
this will most likely be how we are going to all access the project at the same time and make revisions dynamically without causing *too much* problems with merge conflicts. 

With branches your workflow may go like this:

1. Dive in and start making a risky change.
2. At any time before you commit, you can decide to create a new branch.
3. Now your commits cause the new branch to grow, instead of the master branch.
4. If your experiment fails, you can easily return to the master branch.
5. If your experiment succeeds, you can merge the new branch into master.

[git branches guide](https://www.atlassian.com/git/tutorials/using-branches)


#### branch commands and their descriptions

`$ git checkout -b BRANCHNAME`
Create a new branch named BRANCHNAME, and make it the current branch. HEAD still points to the same commit that you were on before, but new commits will now be added to this branch.  New commits will not become a part of the old branch you were on.

`$ git branch`
lists the branches you have checked out in this repo

`$ git branch -a`
List all of the branches in your repo, including branches from any remote repositories you have fetched from.

`$ git show-branch`
List all of the branches you have checked out in this repo, and show which commits belong to each one. Branches contain the same commit when the `*` or `+` symbols are present in each of their columns.

`$ git show-branch --more=10`
Git's show-branch command stops displaying history as soon as it reaches the earliest common ancestor for all commits. The --more argument tells it to go back further, if possible

`$ git branch -d BRANCHNAME`
Safely delete the branch BRANCHNAME.
Git will not allow you to delete a branch whose commits are not also present in another branch.  Use git branch -d after you have successfully merged one of your experimental branches into a permanent branch such as master.

`$ git branch -D BRANCHNAME`
Dangerously delete the branch BRANCHNAME.
Use  git branch -D when you have determined that your experiment has gone terribly wrong, and you're positive that you will never need to even look at this code ever again.
It should be noted that the commits don't actually go anywhere; the name of the branch is the only thing which is truly deleted. But without a name it is difficult for you to recover those lost commits. This is why deleting branches should only be done with care.

`$ git push REMOTE BRANCHNAME`
Send commits in BRANCHNAME to REMOTE.
So far you've only been pushing your master branch to GitLab.  You can push other branches, too.  The GitLab website defaults to showing the master branch, but you can view commits in other branches as well.
Because we see the master branch by default, this is the branch that will be graded.  Make sure that your master branch contains all commits that make up your final submission.

`$ git merge BRANCHNAME`
Make the commits in BRANCHNAME become part of the currently checked-out branch.

If you are following best-practices and keeping all risky work off the master branch, there will come a time when you decide that your experiment is a success that deserves to be publicized.  The merge command is how to conclude a successful experiment.

Generally, you will first run git checkout master or a similar command before merging your work into permanent branch

```
$ git checkout experiment
$ code main.py
...
$ git add main.py
$ git commit -m "Eureka!!!"
$ git checkout master
$ git merge experiment
  
```

--- 

