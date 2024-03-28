CREATE OR REPLACE FUNCTION update_like_count()
RETURNS TRIGGER AS $$
BEGIN
    IF TG_OP = 'INSERT' THEN
        IF NEW."PostId" IS NOT NULL THEN
            UPDATE "Posts" SET "LikeCount" = "LikeCount" + 1 WHERE "Id" = NEW."PostId";
        END IF;
        IF NEW."CommentId" IS NOT NULL THEN
            UPDATE "Comments" SET "LikeCount" = "LikeCount" + 1 WHERE "Id" = NEW."CommentId";
        END IF;
    ELSIF TG_OP = 'DELETE' THEN
        IF OLD."PostId" IS NOT NULL THEN
            UPDATE "Posts" SET "LikeCount" = "LikeCount" - 1 WHERE "Id" = OLD."PostId";
        END IF;
        IF OLD."CommentId" IS NOT NULL THEN
            UPDATE "Comments" SET "LikeCount" = "LikeCount" - 1 WHERE "Id" = OLD."CommentId";
        END IF;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE 'plpgsql';

CREATE TRIGGER update_likes_after_insert_or_delete
AFTER INSERT OR DELETE ON "Likes"
FOR EACH ROW EXECUTE PROCEDURE update_like_count();



CREATE OR REPLACE FUNCTION update_comment_counts()
RETURNS TRIGGER AS $$
BEGIN
    IF TG_OP = 'INSERT' THEN
        IF NEW."RepliedCommentId" IS NOT NULL THEN
            UPDATE "Comments" SET "ReplyCount" = COALESCE("ReplyCount", 0) + 1 WHERE "Id" = NEW."RepliedCommentId";
        END IF;
        UPDATE "Posts" SET "CommentCount" = "CommentCount" + 1 WHERE "Id" = NEW."PostId";
    ELSIF TG_OP = 'DELETE' THEN
        IF OLD."RepliedCommentId" IS NOT NULL THEN
            UPDATE "Comments" SET "ReplyCount" = "ReplyCount" - 1 WHERE "Id" = OLD."RepliedCommentId";
        END IF;
        UPDATE "Posts" SET "CommentCount" = "CommentCount" - 1 WHERE "Id" = OLD."PostId";
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE 'plpgsql';

CREATE TRIGGER update_comment_counts_after_insert_or_delete
AFTER INSERT OR DELETE ON "Comments"
FOR EACH ROW EXECUTE PROCEDURE update_comment_counts();



CREATE OR REPLACE FUNCTION update_follow_counts()
RETURNS TRIGGER AS $$
BEGIN
    IF TG_OP = 'INSERT' THEN
        UPDATE "Users" SET "FollowerCount" = "FollowerCount" + 1 WHERE "Id" = NEW."FolloweeId";
        UPDATE "Users" SET "FolloweeCount" = "FolloweeCount" + 1 WHERE "Id" = NEW."FollowerId";
    ELSIF TG_OP = 'DELETE' THEN
        UPDATE "Users" SET "FollowerCount" = "FollowerCount" - 1 WHERE "Id" = OLD."FolloweeId";
        UPDATE "Users" SET "FolloweeCount" = "FolloweeCount" - 1 WHERE "Id" = OLD."FollowerId";
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE 'plpgsql';

CREATE TRIGGER update_follow_counts_after_insert_or_delete
AFTER INSERT OR DELETE ON "Friendships"
FOR EACH ROW EXECUTE PROCEDURE update_follow_counts();



CREATE OR REPLACE FUNCTION update_post_counts()
RETURNS TRIGGER AS $$
BEGIN
    IF TG_OP = 'INSERT' THEN
        UPDATE "Tags" SET "PostCount" = "PostCount" + 1 WHERE "Id" = NEW."TagId";
    ELSIF TG_OP = 'DELETE' THEN
        UPDATE "Tags" SET "PostCount" = "PostCount" - 1 WHERE "Id" = OLD."TagId";
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE 'plpgsql';

CREATE TRIGGER update_post_counts_after_insert_or_delete
AFTER INSERT OR DELETE ON "PostTag"
FOR EACH ROW EXECUTE PROCEDURE update_post_counts();