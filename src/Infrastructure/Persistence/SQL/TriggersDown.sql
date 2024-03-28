DROP TRIGGER IF EXISTS update_likes_after_insert_or_delete ON "Likes";
DROP FUNCTION IF EXISTS update_like_count();

DROP TRIGGER IF EXISTS update_comment_counts_after_insert_or_delete ON "Comments";
DROP FUNCTION IF EXISTS update_comment_counts();

DROP TRIGGER IF EXISTS update_follow_counts_after_insert_or_delete ON "Friendships";
DROP FUNCTION IF EXISTS update_follow_counts();

DROP TRIGGER IF EXISTS update_post_counts_after_insert_or_delete ON "PostTag";
DROP FUNCTION IF EXISTS update_post_counts();