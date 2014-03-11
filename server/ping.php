<?php
  file_put_contents('test.txt', file_get_contents('php://input'), FILE_APPEND);
  file_put_contents('test.txt', PHP_EOL, FILE_APPEND);
?>
